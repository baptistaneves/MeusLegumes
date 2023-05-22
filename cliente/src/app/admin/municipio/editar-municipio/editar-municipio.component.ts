import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { Municipio } from '../../models/provincias/municipio';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBaseComponent } from 'src/app/base-component/form-base.component';
import { ProvinciaService } from '../../services/provincias/provincia.service';
import { Provincia } from '../../models/provincias/provincia';

@Component({
  selector: 'app-editar-municipio',
  templateUrl: './editar-municipio.component.html'
})
export class EditarMunicipioComponent extends FormBaseComponent implements OnInit, AfterViewInit  {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  municipioForm: FormGroup;
  municipio: Municipio = new Municipio();
  provincias: Provincia[];

  constructor(private fb: FormBuilder,
              private provinciaService: ProvinciaService,
              private toastr: ToastrService,
              private router: Router,
              private route: ActivatedRoute) {
      
      super();

      this.validationMessages = {
        nome: {
          required: "Informe o nome"
        },
        provinciaId: {
          required: "Selecione a província"
        }
      };

      super.configurarMensagensValidacaoBase(this.validationMessages);

      this.municipio = this.route.snapshot.data['municipio'];
  }

  ngOnInit(): void {
    this.listarProvincias();
    this.inicializarFormulario();
    this.preencherForm();
  }

  listarProvincias() {
    this.provinciaService.obterProvincias()
    .subscribe(
      provincias => this.provincias = provincias,
      erros => this.errors
    )
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.municipioForm);
  }

  preencherForm() {
    this.municipioForm.patchValue({
      nome: this.municipio.nome,
      provinciaId: this.municipio.provinciaId,
    });
  }

  inicializarFormulario() {
    this.municipioForm = this.fb.group({
      nome: ['', [Validators.required]],
      provinciaId: ['', [Validators.required]]
    });
  }

  editar() {
    if(this.municipioForm.dirty && this.municipioForm.valid) {
      this.municipio = Object.assign({}, this.municipio, this.municipioForm.value);

      this.provinciaService.editarMunicipio(this.municipio)
      .subscribe(
        sucesso => { this.processarSucesso() },
        erros => { this.processarFalha(erros) }
      );
    }
  }

  processarSucesso() {
    this.errors = [];

    let toast = this.toastr.success('Município Alterado com Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/admin/municipios']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

}
