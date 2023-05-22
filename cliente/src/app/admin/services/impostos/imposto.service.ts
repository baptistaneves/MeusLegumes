import { Injectable } from '@angular/core';
import { Imposto } from '../../models/impostos/imposto';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, map } from 'rxjs';
import { BaseService } from 'src/app/services/base.service';

@Injectable({
  providedIn: 'root'
})
export class ImpostoService extends BaseService {

  constructor(private http: HttpClient) { super();  }

  obterTodos() : Observable<Imposto[]>{
    return this.http
          .get<Imposto[]>(this.UrlServiceV1 + "impostos/obter-impostos", this.ObterAuthHeaderJson())
          .pipe(catchError(super.serviceError));
  }

  obterPorId(id:string) : Observable<Imposto>{
    return this.http
      .get<Imposto>(this.UrlServiceV1 + "impostos/obter-imposto-por-id/" + id, this.ObterAuthHeaderJson())
      .pipe(catchError(super.serviceError));
  }

  adicionar(imposto: Imposto) : Observable<Imposto>{
    let response = this.http
        .post<Imposto>(this.UrlServiceV1 + "impostos/novo-imposto", imposto, this.ObterAuthHeaderJson())
        .pipe((
          map(this.extractData),
          catchError(this.serviceError)));

    return response;
  }

  editar(imposto: Imposto): Observable<Imposto> {
    return this.http
        .put(this.UrlServiceV1 + "impostos/actualizar-imposto", imposto, super.ObterAuthHeaderJson())
        .pipe(
            map(super.extractData),
            catchError(super.serviceError));
}

excluir(id: string): Observable<Imposto> {
    return this.http
        .delete(this.UrlServiceV1 + "impostos/remover-imposto/" + id, super.ObterAuthHeaderJson())
        .pipe(
            map(super.extractData),
            catchError(super.serviceError));
}

}
