import { Injectable } from '@angular/core';
import {Resolve, ActivatedRouteSnapshot} from '@angular/router';
import { Produto } from '../../models/produtos/produto';
import { ProdutoService } from './produto.service';

@Injectable({
  providedIn: 'root'
})
export class ProdutoResolver implements Resolve<Produto> {
  constructor(private produtoService: ProdutoService) {}

  resolve(route: ActivatedRouteSnapshot){
    return this.produtoService.obterPorId(route.params['id']);
  }
}
