import { ToastrService } from 'ngx-toastr';
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';

import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";

import { LocalStorageUtils } from '../utils/localstorage';
import { Router } from '@angular/router';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private router: Router,
                private toastr: ToastrService) { }

    localStorageUtil = new LocalStorageUtils();

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return next.handle(req).pipe(catchError(error => {

            if (error instanceof HttpErrorResponse) {

                if (error.status === 401) {
                    this.localStorageUtil.limparDadosLocaisUsuario();
                    this.router.navigate(['/admin/login'], { queryParams: { returnUrl: this.router.url }});
                }
                if (error.status === 403) {
                  this.toastr.warning('Acesso Negado!', 'Opa :(');
                }
                // if (error.status === 500) {
                //   this.toastr.warning('Erro Interno no Servidor!', 'Opa :(');
                // }
            }

            return throwError(error);
        }));
    }

}