import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { Imposto } from '../../models/impostos/imposto';
import { ImpostoService } from '../../services/impostos/imposto.service';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBaseComponent } from 'src/app/base-component/form-base.component';
import { CustomValidators } from '@narik/custom-validators';

@Component({
  selector: 'app-novo-imposto',
  templateUrl: './novo-imposto.component.html'
})
export class NovoImpostoComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  cadastroForm: FormGroup;
  imposto: Imposto;

  constructor(private fb: FormBuilder,
              private impostoService: ImpostoService,
              private toastr: ToastrService,
              private router: Router) {
      
      super();

      this.validationMessages = {
        descricao: {
          required: "Informe a descrição"
        },
        taxa: {
          required: "Informe a taxa",
          number: "Informe somente números"
        },
        tipoDeTaxa: {
          required: "Informe o tipo de taxa"
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
      descricao: ['', [Validators.required]],
      taxa: ['', [Validators.required, CustomValidators.number]],
      tipoDeTaxa: ['', [Validators.required]]
    });
  }

  adicionar() {
    if(this.cadastroForm.dirty && this.cadastroForm.valid) {
      this.imposto = Object.assign({}, this.imposto, this.cadastroForm.value);

      this.impostoService.adicionar(this.imposto)
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
        this.router.navigate(['/admin/impostos']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

}
