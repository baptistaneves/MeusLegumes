import { AfterViewInit, Component, OnInit, ViewChildren, ElementRef  } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, FormControlName } from '@angular/forms';


import { FormBaseComponent } from 'src/app/base-component/form-base.component';
import { Usuario } from '../../models/usuarios/usuario';
import { UsuarioService } from '../../services/usuarios/usuario.service';

import { CustomValidators } from '@narik/custom-validators';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-novo-usuario',
  templateUrl: './novo-usuario.component.html'
})
export class NovoUsuarioComponent extends FormBaseComponent implements OnInit, AfterViewInit{

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  cadastroForm: FormGroup;
  usuario: Usuario;

  constructor(private fb: FormBuilder,
              private usuarioService: UsuarioService,
              private toastr: ToastrService,
              private router: Router) {
      
      super();

      this.validationMessages = {
        email: {
          required: "Informe o e-mail",
          email: "E-mail inválido"
        },
        nome: {
          required: "Informe o nome",
        },
        perfil: {
          required: "Informe o perfil"
        },
        senha: {
          required: "Informe a senha",
        },
        confirmarSenha: {
          required: "Informe a senha novamente",
          equalTo: "As senhas não conferem"
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
    let password = new FormControl('', [Validators.required]);
    let confirmPassword = new FormControl('',  [Validators.required, CustomValidators.equalTo(password)]);

    this.cadastroForm = this.fb.group({
      nome: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      perfil: ['', [Validators.required]],
      senha: password,
      confirmarSenha: confirmPassword
    });
  }

  adicionar() {
    if(this.cadastroForm.dirty && this.cadastroForm.valid) {
      this.usuario = Object.assign({}, this.usuario, this.cadastroForm.value);

      this.usuarioService.adicionar(this.usuario)
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
        this.router.navigate(['/admin/usuarios']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }
}
