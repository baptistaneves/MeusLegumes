import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';

import { Usuario } from '../../models/usuarios/usuario';
import { UsuarioService } from '../../services/usuarios/usuario.service';
import { Modal } from 'src/app/utils/modal';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { AlterarSenha } from '../../models/usuarios/alterarSenha';
import { FormBaseComponent } from 'src/app/base-component/form-base.component';

@Component({
  selector: 'app-listar-usuarios',
  templateUrl: './listar-usuarios.component.html'
})
export class ListarUsuariosComponent extends FormBaseComponent implements OnInit, AfterViewInit{
  
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  usuarios: Usuario[];
  usuarioId: string;
  errorMessage: string;

  alterarSenhaForm: FormGroup;
  alterarSenhaModel: AlterarSenha = new AlterarSenha();

  constructor(private usuarioService: UsuarioService,
              private toastr: ToastrService,
              private fb: FormBuilder,) {

      super();

      this.validationMessages = {
        senhaActual: {
          required: "Informe a senha actual"
        },
        novaSenha: {
          required: "Informe a nova senha",
        }
      };

      super.configurarMensagensValidacaoBase(this.validationMessages);

  }
  
  ngOnInit(): void {
    this.listarUsuarios();
    this.inicializarFormulario();
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.alterarSenhaForm);
  }

  inicializarFormulario() {
    this.alterarSenhaForm = this.fb.group({
      senhaActual: ['', [Validators.required]],
      novaSenha: ['', [Validators.required]]
    });
  }

  listarUsuarios() {
    this.usuarioService.obterTodos()
          .subscribe(
            usuarios => this.usuarios = usuarios,
            erros => this.errorMessage
          )
  }

  abrirModal(id: string){
    this.usuarioId = id;
  }

  excluir(){
    this.usuarioService.excluir(this.usuarioId)
    .subscribe(
      sucesso => { this.processarSucessoExcluir() },
      erros => { this.processarFalha(erros) }
    );
   
  }

  
  alterarSenha() {
    if(this.alterarSenhaForm.dirty && this.alterarSenhaForm.valid) {
      this.alterarSenhaModel = Object.assign({}, this.alterarSenhaModel, this.alterarSenhaForm.value);
      this.alterarSenhaModel.id = this.usuarioId;

      this.usuarioService.alterarSenha(this.alterarSenhaModel)
            .subscribe(
              sucesso => { this.processarSucessoAlterarSenha() },
              erros => { this.processarFalha(erros) }
            );
    }
  }

  processarSucessoAlterarSenha() {
    Modal.fecharModal("modalAlterarSenha");
    this.alterarSenhaForm.reset();
    this.errors = [];

    let toast = this.toastr.success('Senha Alterada com Sucesso!');
  }

  processarSucessoExcluir() {
    Modal.fecharModal("modalExcluir");
    this.errors = [];

    this.toastr.success('Excluido com Sucesso!');
    this.listarUsuarios();
  }

  processarFalha(fail: any) {
    Modal.fecharModal("modalExcluir");
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }
}
