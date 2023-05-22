import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { Municipio } from '../../models/provincias/municipio';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBaseComponent } from 'src/app/base-component/form-base.component';
import { ProvinciaService } from '../../services/provincias/provincia.service';
import { Provincia } from '../../models/provincias/provincia';

@Component({
  selector: 'app-novo-municipio',
  templateUrl: './novo-municipio.component.html'
})
export class NovoMunicipioComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  cadastroForm: FormGroup;
  municipio: Municipio;
  provincias: Provincia[];

  constructor(private fb: FormBuilder,
              private provinciaService: ProvinciaService,
              private toastr: ToastrService,
              private router: Router) {
      
      super();

      this.validationMessages = {
        nome: {
          required: "Informe o nome"
        },
        provinciaId: {
          required: "Seleciona a província"
        }
      };

      super.configurarMensagensValidacaoBase(this.validationMessages);
  }

  ngOnInit(): void {
    this.listarProvincias();
    this.inicializarFormulario();
  }

  listarProvincias() {
    this.provinciaService.obterProvincias()
    .subscribe(
      provincias => this.provincias = provincias,
      erros => this.errors
    )
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.cadastroForm);
  }

  inicializarFormulario() {

    this.cadastroForm = this.fb.group({
      nome: ['', [Validators.required]],
      provinciaId: ['', [Validators.required]]
    });
  }

  adicionar() {
    if(this.cadastroForm.dirty && this.cadastroForm.valid) {
      this.municipio = Object.assign({}, this.municipio, this.cadastroForm.value);

      this.provinciaService.adicionarMunicipio(this.municipio)
            .subscribe(
              sucesso => { this.processarSucesso() },
              erros => { this.processarFalha(erros) }
            );
    }
  }

  processarSucesso() {
    this.cadastroForm.reset();
    this.errors = [];

    let toast = this.toastr.success('Cadastro realizado com Sucesso!');
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
