import { Component, ElementRef, OnInit } from '@angular/core';

import { Usuario } from '../../models/usuarios/usuario';
import { UsuarioService } from '../../services/usuarios/usuario.service';
import { Modal } from 'src/app/utils/modal';

@Component({
  selector: 'app-listar-usuarios',
  templateUrl: './listar-usuarios.component.html'
})
export class ListarUsuariosComponent implements OnInit{
  
  usuarios: Usuario[];
  usuarioId: string;
  errorMessage: string;

  constructor(private usuarioService: UsuarioService) {}
  
  ngOnInit(): void {
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
    Modal.fecharModal("modalExcluir");
  }

}
