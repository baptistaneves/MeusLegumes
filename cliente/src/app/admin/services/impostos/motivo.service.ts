import { Injectable } from '@angular/core';
import { Motivo } from '../../models/impostos/motivo';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, map } from 'rxjs';
import { BaseService } from 'src/app/services/base.service';

@Injectable({
  providedIn: 'root'
})
export class MotivoService extends BaseService {

  constructor(private http: HttpClient) { super();  }

  obterTodos() : Observable<Motivo[]>{
    return this.http
          .get<Motivo[]>(this.UrlServiceV1 + "motivosIsencaoIva/obter-motivos", this.ObterAuthHeaderJson())
          .pipe(catchError(super.serviceError));
  }

  obterPorId(id:string) : Observable<Motivo>{
    return this.http
      .get<Motivo>(this.UrlServiceV1 + "motivosIsencaoIva/obter-motivo-por-id/" + id, this.ObterAuthHeaderJson())
      .pipe(catchError(super.serviceError));
  }

  adicionar(motivo: Motivo) : Observable<Motivo>{
    let response = this.http
        .post<Motivo>(this.UrlServiceV1 + "motivosIsencaoIva/novo-motivo", motivo, this.ObterAuthHeaderJson())
        .pipe((
          map(this.extractData),
          catchError(this.serviceError)));

    return response;
  }

  editar(motivo: Motivo): Observable<Motivo> {
    return this.http
        .put(this.UrlServiceV1 + "motivosIsencaoIva/actualizar-motivo", motivo, super.ObterAuthHeaderJson())
        .pipe(
            map(super.extractData),
            catchError(super.serviceError));
}

excluir(id: string): Observable<Motivo> {
    return this.http
        .delete(this.UrlServiceV1 + "motivosIsencaoIva/remover-motivo/" + id, super.ObterAuthHeaderJson())
        .pipe(
            map(super.extractData),
            catchError(super.serviceError));
}

}
