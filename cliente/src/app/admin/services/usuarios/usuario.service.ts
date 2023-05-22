import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, map } from 'rxjs';

import { Login } from './../../models/usuarios/login';
import { Usuario } from './../../models/usuarios/usuario';
import { BaseService } from 'src/app/services/base.service';
import { UsuarioResponse } from '../../models/usuarios/usuario-response';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService extends BaseService {

  constructor(private http: HttpClient) { super();  }

  obterTodos() : Observable<Usuario[]>{
    return this.http
          .get<Usuario[]>(this.UrlServiceV1 + "usuarios/obter-usuarios", this.ObterAuthHeaderJson())
          .pipe(catchError(super.serviceError));
  }

  obterPorId(id:string) : Observable<Usuario>{
    return this.http
      .get<Usuario>(this.UrlServiceV1 + "usuarios/obter-usuario-por-id/" + id, this.ObterAuthHeaderJson())
      .pipe(catchError(super.serviceError));
  }

  adicionar(usuario: Usuario) : Observable<Usuario>{
    let response = this.http
        .post<Usuario>(this.UrlServiceV1 + "usuarios/criar-usuario", usuario, this.ObterAuthHeaderJson())
        .pipe((
          map(this.extractData),
          catchError(this.serviceError)));

    return response;
  }

  login(login: Login) : Observable<UsuarioResponse>{
    let response = this.http
        .post<UsuarioResponse>(this.UrlServiceV1 + "login/login", login, this.ObterHeaderJson())
        .pipe((
          map(this.extractData),
          catchError(this.serviceError)));

    return response;
  }
}
