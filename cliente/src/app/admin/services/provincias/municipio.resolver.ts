import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';

import { Municipio } from '../../models/provincias/municipio';
import { ProvinciaService } from './provincia.service';

@Injectable({
  providedIn: 'root'
})
export class MunicipioResolver implements Resolve<Municipio> {
  constructor(private provinciaService: ProvinciaService) {}

  resolve(route: ActivatedRouteSnapshot) {
    return this.provinciaService.obterMunicipioPorId(route.params['id']);
  }
}
