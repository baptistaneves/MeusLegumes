import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Usuario } from '../../models/usuarios/usuario';
import { UsuarioService } from '../../services/usuarios/usuario.service';

@Component({
  selector: 'app-detalhes-usuario',
  templateUrl: './detalhes-usuario.component.html'
})
export class DetalhesUsuarioComponent {

  usuarioForm: FormGroup;
  usuario: Usuario = new Usuario();

  constructor(private fb: FormBuilder,
              private usuarioService: UsuarioService,
              private route: ActivatedRoute) {
      
      this.usuario = this.route.snapshot.data['usuario'];
  }

  ngOnInit(): void {
    this.inicializarFormulario();
    this.preencherForm();
  }

  preencherForm() {
    this.usuarioForm.patchValue({
      nome: this.usuario.nome,
      email: this.usuario.email,
      perfil: this.usuario.perfil
    });

    this.usuarioForm.disable();
  }

  inicializarFormulario() {
    this.usuarioForm = this.fb.group({
      nome: ['',  [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      perfil: ['', [Validators.required]]
    });
  }

}
