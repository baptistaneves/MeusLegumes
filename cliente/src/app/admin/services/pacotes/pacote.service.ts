import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map } from 'rxjs';
import { BaseService } from 'src/app/services/base.service';
import { Pacote } from '../../models/pacotes/pacote';

@Injectable({
  providedIn: 'root'
})
export class PacoteService extends BaseService {

  constructor(private http: HttpClient) { super();  }

  obterTodos() : Observable<Pacote[]>{
    return this.http
          .get<Pacote[]>(this.UrlServiceV1 + "pacotes/obter-pacotes", this.ObterAuthHeaderJson())
          .pipe(catchError(super.serviceError));
  }

  obterPorId(id:string) : Observable<Pacote>{
    return this.http
      .get<Pacote>(this.UrlServiceV1 + "pacotes/obter-pacote-por-id/" + id, this.ObterAuthHeaderJson())
      .pipe(catchError(super.serviceError));
  }

  adicionar(pacote: Pacote) : Observable<Pacote>{
    let response = this.http
        .post<Pacote>(this.UrlServiceV1 + "pacotes/novo-pacote", pacote, this.ObterAuthHeaderJson())
        .pipe((
          map(this.extractData),
          catchError(this.serviceError)));

    return response;
  }

  editar(pacote: Pacote): Observable<Pacote> {
    return this.http
        .put(this.UrlServiceV1 + "pacotes/actualizar-pacote", pacote, super.ObterAuthHeaderJson())
        .pipe(
            map(super.extractData),
            catchError(super.serviceError));
}

excluir(id: string): Observable<Pacote> {
    return this.http
        .delete(this.UrlServiceV1 + "pacotes/remover-pacote/" + id, super.ObterAuthHeaderJson())
        .pipe(
            map(super.extractData),
            catchError(super.serviceError));
}
}
