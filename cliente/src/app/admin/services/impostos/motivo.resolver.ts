import { Injectable } from '@angular/core';
import {Resolve, ActivatedRouteSnapshot} from '@angular/router';
import { Motivo } from '../../models/impostos/motivo';
import { MotivoService } from './motivo.service';

@Injectable({
  providedIn: 'root'
})
export class MotivoResolver implements Resolve<Motivo> {
  constructor(private motivoService: MotivoService) {}

  resolve(route: ActivatedRouteSnapshot) {
    return this.motivoService.obterPorId(route.params['id']);
  }
}
