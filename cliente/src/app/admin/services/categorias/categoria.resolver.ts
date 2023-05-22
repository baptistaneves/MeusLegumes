import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Categoria } from '../../models/categorias/categoria';
import { CategoriaService } from './categoria.service';

@Injectable({
  providedIn: 'root'
})
export class CategoriaResolver implements Resolve<Categoria> {

  constructor(private categoriaService: CategoriaService) {}

  resolve(route: ActivatedRouteSnapshot) {
    return this.categoriaService.obterPorId(route.params['id']);
  }
}
