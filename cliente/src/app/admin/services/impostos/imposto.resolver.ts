import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Imposto } from '../../models/impostos/imposto';
import { ImpostoService } from './imposto.service';

@Injectable({
  providedIn: 'root'
})
export class ImpostoResolver implements Resolve<Imposto> {
  constructor(private impostoService: ImpostoService) {}

  resolve(route: ActivatedRouteSnapshot) {
    return this.impostoService.obterPorId(route.params['id']);
  }
}
