import { Injectable } from '@angular/core';
import {Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Unidade } from '../../models/unidades/unidade';
import { UnidadeService } from './unidade.service';

@Injectable({
  providedIn: 'root'
})
export class UnidadeResolver implements Resolve<Unidade> {
  constructor(private unidadeService: UnidadeService) {}

  resolve(route: ActivatedRouteSnapshot) {
    return this.unidadeService.obterPorId(route.params['id']);
  }
}
