import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { Unidade } from '../../models/unidades/unidade';
import { UnidadeService } from '../../services/unidades/unidade.service';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBaseComponent } from 'src/app/base-component/form-base.component';

@Component({
  selector: 'app-nova-unidade',
  templateUrl: './nova-unidade.component.html'
})
export class NovaUnidadeComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  cadastroForm: FormGroup;
  unidade: Unidade;

  constructor(private fb: FormBuilder,
              private unidadeService: UnidadeService,
              private toastr: ToastrService,
              private router: Router) {
      
      super();

      this.validationMessages = {
        descricao: {
          required: "Informe a descrição"
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
      descricao: ['', [Validators.required]]
    });
  }

  adicionar() {
    if(this.cadastroForm.dirty && this.cadastroForm.valid) {
      this.unidade = Object.assign({}, this.unidade, this.cadastroForm.value);

      this.unidadeService.adicionar(this.unidade)
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
        this.router.navigate(['/admin/unidades']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }


}
