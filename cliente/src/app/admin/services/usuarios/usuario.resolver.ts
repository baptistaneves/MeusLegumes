import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Usuario } from '../../models/usuarios/usuario';
import { UsuarioService } from './usuario.service';

@Injectable({
  providedIn: 'root'
})
export class UsuarioResolver implements Resolve<Usuario> {
  constructor(private usuarioService: UsuarioService) { }

  resolve(route: ActivatedRouteSnapshot){
    return this.usuarioService.obterPorId(route.params['id']);
  }
}
