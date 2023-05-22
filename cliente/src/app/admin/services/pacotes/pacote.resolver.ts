import { Injectable } from '@angular/core';
import {Resolve, ActivatedRouteSnapshot} from '@angular/router';
import { Observable, of } from 'rxjs';
import { Pacote } from '../../models/pacotes/pacote';
import { PacoteService } from './pacote.service';

@Injectable({
  providedIn: 'root'
})
export class PacoteResolver implements Resolve<Pacote> {
  constructor(private pacoteService: PacoteService) {}

  resolve(route: ActivatedRouteSnapshot) {
    return this.pacoteService.obterPorId(route.params['id']);
  }
}
