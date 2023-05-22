import { Injectable } from '@angular/core';
import { Unidade } from '../../models/unidades/unidade';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, map } from 'rxjs';
import { BaseService } from 'src/app/services/base.service';
import { Categoria } from '../../models/categorias/categoria';

@Injectable({
  providedIn: 'root'
})
export class UnidadeService extends BaseService {

  constructor(private http: HttpClient) { super();  }

  obterTodos() : Observable<Unidade[]>{
    return this.http
          .get<Unidade[]>(this.UrlServiceV1 + "unidades/obter-unidades", this.ObterAuthHeaderJson())
          .pipe(catchError(super.serviceError));
  }

  obterPorId(id:string) : Observable<Unidade>{
    return this.http
      .get<Unidade>(this.UrlServiceV1 + "unidades/obter-unidade-por-id/" + id, this.ObterAuthHeaderJson())
      .pipe(catchError(super.serviceError));
  }

  adicionar(unidade: Unidade) : Observable<Unidade>{
    let response = this.http
        .post<Categoria>(this.UrlServiceV1 + "unidades/nova-unidade", unidade, this.ObterAuthHeaderJson())
        .pipe((
          map(this.extractData),
          catchError(this.serviceError)));

    return response;
  }

  editar(categoria: Unidade): Observable<Unidade> {
    return this.http
        .put(this.UrlServiceV1 + "unidades/actualizar-unidade", categoria, super.ObterAuthHeaderJson())
        .pipe(
            map(super.extractData),
            catchError(super.serviceError));
}

excluir(id: string): Observable<Unidade> {
    return this.http
        .delete(this.UrlServiceV1 + "unidades/remover-unidade/" + id, super.ObterAuthHeaderJson())
        .pipe(
            map(super.extractData),
            catchError(super.serviceError));
}
}
