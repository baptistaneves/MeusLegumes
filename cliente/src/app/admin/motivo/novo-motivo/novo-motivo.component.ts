import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormBaseComponent } from 'src/app/base-component/form-base.component';
import { Motivo } from '../../models/impostos/motivo';
import { MotivoService } from '../../services/impostos/motivo.service';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-novo-motivo',
  templateUrl: './novo-motivo.component.html'
})
export class NovoMotivoComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  cadastroForm: FormGroup;
  motivo: Motivo;

  constructor(private fb: FormBuilder,
              private motivoService: MotivoService,
              private toastr: ToastrService,
              private router: Router) {
      
      super();

      this.validationMessages = {
        codigoInterno: {
          required: "Informe o código"
        },
        mencaoFactura: {
          required: "Informe a menção"
        },
        normaLegalAplicavel: {
          required: "Informe a norma"
        },
        motivo: {
          required: "Informe o motivo"
        }

      };

      super.configurarMensagensValidacaoBase(this.validationMessages);
  }

  ngOnInit(): void {
    this.inicializarFormulario();
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.cadastroForm);
  }

  inicializarFormulario() {

    this.cadastroForm = this.fb.group({
      codigoInterno: ['', [Validators.required]],
      mencaoFactura: ['', [Validators.required]],
      normaLegalAplicavel: ['', [Validators.required]],
      motivo: ['', [Validators.required]]
    });
  }

  adicionar() {
    if(this.cadastroForm.dirty && this.cadastroForm.valid) {
      this.motivo = Object.assign({}, this.motivo, this.cadastroForm.value);

      this.motivoService.adicionar(this.motivo)
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
        this.router.navigate(['/admin/motivos']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

}
