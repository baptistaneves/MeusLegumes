import { Injectable } from '@angular/core';
import {Resolve, ActivatedRouteSnapshot } from '@angular/router';

import { Provincia } from './../../models/provincias/provincia';
import { ProvinciaService } from './provincia.service';

@Injectable({
  providedIn: 'root'
})
export class ProvinciaResolver implements Resolve<Provincia> {
  constructor(private provinciaService: ProvinciaService) {}

  resolve(route: ActivatedRouteSnapshot) {
    return this.provinciaService.obterProvinciaPorId(route.params['id']);
  }
}
