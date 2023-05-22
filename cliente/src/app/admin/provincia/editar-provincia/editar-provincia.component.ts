import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { Provincia } from '../../models/provincias/provincia';
import { ProvinciaService } from '../../services/provincias/provincia.service';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBaseComponent } from 'src/app/base-component/form-base.component';

@Component({
  selector: 'app-editar-provincia',
  templateUrl: './editar-provincia.component.html'
})
export class EditarProvinciaComponent extends FormBaseComponent implements OnInit, AfterViewInit {
  
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  provinciaForm: FormGroup;
  provincia: Provincia = new Provincia();

  constructor(private fb: FormBuilder,
              private provinciaService: ProvinciaService,
              private toastr: ToastrService,
              private router: Router,
              private route: ActivatedRoute) {
      
      super();

      this.validationMessages = {
        nome: {
          required: "Informe o nome"
        }
      };

      super.configurarMensagensValidacaoBase(this.validationMessages);

      this.provincia = this.route.snapshot.data['provincia'];
  }

  ngOnInit(): void {
    this.inicializarFormulario();
    this.preencherForm();
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.provinciaForm);
  }

  preencherForm() {
    this.provinciaForm.patchValue({
      nome: this.provincia.nome,
    });
  }

  inicializarFormulario() {
    this.provinciaForm = this.fb.group({
      nome: ['', [Validators.required]]
    });
  }

  editar() {
    if(this.provinciaForm.dirty && this.provinciaForm.valid) {
      this.provincia = Object.assign({}, this.provincia, this.provinciaForm.value);

      this.provinciaService.editarProvincia(this.provincia)
      .subscribe(
        sucesso => { this.processarSucesso() },
        erros => { this.processarFalha(erros) }
      );
    }
  }

  processarSucesso() {
    this.errors = [];

    let toast = this.toastr.success('Província Alterada com Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/admin/provincias']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

}
