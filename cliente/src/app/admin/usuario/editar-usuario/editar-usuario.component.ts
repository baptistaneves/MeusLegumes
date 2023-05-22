import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { FormBaseComponent } from 'src/app/base-component/form-base.component';
import { Usuario } from '../../models/usuarios/usuario';
import { UsuarioService } from '../../services/usuarios/usuario.service';

@Component({
  selector: 'app-editar-usuario',
  templateUrl: './editar-usuario.component.html'
})
export class EditarUsuarioComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  usuarioForm: FormGroup;
  usuario: Usuario = new Usuario();

  constructor(private fb: FormBuilder,
              private usuarioService: UsuarioService,
              private toastr: ToastrService,
              private router: Router,
              private route: ActivatedRoute) {
      
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
        }
      };

      super.configurarMensagensValidacaoBase(this.validationMessages);

      this.usuario = this.route.snapshot.data['usuario'];
  }

  ngOnInit(): void {
    this.inicializarFormulario();
    this.preencherForm();
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.usuarioForm);
  }

  preencherForm() {
    this.usuarioForm.patchValue({
      nome: this.usuario.nome,
      email: this.usuario.email,
      perfil: this.usuario.perfil
    });
  }

  inicializarFormulario() {
    this.usuarioForm = this.fb.group({
      nome: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      perfil: ['', [Validators.required]]
    });
  }

  editar() {
    if(this.usuarioForm.dirty && this.usuarioForm.valid) {
      this.usuario = Object.assign({}, this.usuario, this.usuarioForm.value);
    }
  }

  processarSucesso() {
    this.usuarioForm.reset();
    this.errors = [];

    let toast = this.toastr.success('Usuario Alterado com Sucesso!');
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
