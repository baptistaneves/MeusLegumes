import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Modal } from 'src/app/utils/modal';
import { Produto } from '../../models/produtos/produto';
import { ProdutoService } from '../../services/produtos/produto.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-listar-produtos',
  templateUrl: './listar-produtos.component.html'
})
export class ListarProdutosComponent {

  imagens: string = environment.imagensUrl;

  errors: any[] = [];
  produtos: Produto[];
  produtoId: string;
  errorMessage: string;

  constructor(private produtoService: ProdutoService,
              private toastr: ToastrService) {}
  
  ngOnInit(): void {
   this.listarProdutos();
  }

  listarProdutos() {
    this.produtoService.obterTodos()
    .subscribe(
      produtos => this.produtos = produtos,
      erros => this.errorMessage
    )
  }

  abrirModal(id: string){
    this.produtoId = id;
  }

  excluir(){
    this.produtoService.excluir(this.produtoId)
    .subscribe(
      sucesso => { this.processarSucesso() },
      erros => { this.processarFalha(erros) }
    );
   
  }

  processarSucesso() {
    Modal.fecharModal("modalExcluir");
    this.errors = [];

    this.toastr.success('Excluido com Sucesso!');
    this.listarProdutos();
  }

  processarFalha(fail: any) {
    Modal.fecharModal("modalExcluir");
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }


}
