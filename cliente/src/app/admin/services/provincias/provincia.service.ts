import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, map } from 'rxjs';
import { BaseService } from 'src/app/services/base.service';
import { Categoria } from '../../models/categorias/categoria';
import { Provincia } from '../../models/provincias/provincia';
import { Municipio } from '../../models/provincias/municipio';

@Injectable({
  providedIn: 'root'
})
export class ProvinciaService extends BaseService {

  constructor(private http: HttpClient) { super();  }

  obterProvincias() : Observable<Provincia[]>{
    return this.http
          .get<Provincia[]>(this.UrlServiceV1 + "provincias/obter-provincias", this.ObterAuthHeaderJson())
          .pipe(catchError(super.serviceError));
  }

  obterProvinciaPorId(id:string) : Observable<Provincia>{
    return this.http
      .get<Provincia>(this.UrlServiceV1 + "provincias/obter-provincia-por-id/" + id, this.ObterAuthHeaderJson())
      .pipe(catchError(super.serviceError));
  }

  adicionarProvincia(provincia: Provincia) : Observable<Provincia>{
    let response = this.http
        .post<Provincia>(this.UrlServiceV1 + "provincias/nova-provincia", provincia, this.ObterAuthHeaderJson())
        .pipe((
          map(this.extractData),
          catchError(this.serviceError)));

    return response;
  }

  editarProvincia(provincia: Provincia): Observable<Provincia> {
    return this.http
        .put(this.UrlServiceV1 + "provincias/actualizar-provincia", provincia, super.ObterAuthHeaderJson())
        .pipe(
            map(super.extractData),
            catchError(super.serviceError));
}

excluirProvincia(id: string): Observable<Categoria> {
    return this.http
        .delete(this.UrlServiceV1 + "provincias/remover-provincia/" + id, super.ObterAuthHeaderJson())
        .pipe(
            map(super.extractData),
            catchError(super.serviceError));
}


obterMunicipios() : Observable<Municipio[]>{
  return this.http
        .get<Municipio[]>(this.UrlServiceV1 + "provincias/obter-municipios", this.ObterAuthHeaderJson())
        .pipe(catchError(super.serviceError));
}

obterMunicipioPorId(id:string) : Observable<Municipio>{
  return this.http
    .get<Municipio>(this.UrlServiceV1 + "provincias/obter-municipio-por-id/" + id, this.ObterAuthHeaderJson())
    .pipe(catchError(super.serviceError));
}

adicionarMunicipio(municipio: Municipio) : Observable<Municipio>{
  let response = this.http
      .post<Municipio>(this.UrlServiceV1 + "provincias/novo-municipio", municipio, this.ObterAuthHeaderJson())
      .pipe((
        map(this.extractData),
        catchError(this.serviceError)));

  return response;
}

editarMunicipio(municipio: Municipio): Observable<Municipio> {
  return this.http
      .put(this.UrlServiceV1 + "provincias/actualizar-municipio", municipio, super.ObterAuthHeaderJson())
      .pipe(
          map(super.extractData),
          catchError(super.serviceError));
}

excluirMunicipio(id: string): Observable<Municipio> {
  return this.http
      .delete(this.UrlServiceV1 + "provincias/remover-municipio/" + id, super.ObterAuthHeaderJson())
      .pipe(
          map(super.extractData),
          catchError(super.serviceError));
}

}
