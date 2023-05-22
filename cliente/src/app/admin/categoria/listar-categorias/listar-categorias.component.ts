import { Component } from '@angular/core';
import { Categoria } from '../../models/categorias/categoria';
import { CategoriaService } from '../../services/categorias/categoria.service';
import { Modal } from 'src/app/utils/modal';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-listar-categorias',
  templateUrl: './listar-categorias.component.html'
})
export class ListarCategoriasComponent {

  errors: any[] = [];
  categorias: Categoria[];
  categoriaId: string;
  errorMessage: string;

  constructor(private categoriaService: CategoriaService,
              private toastr: ToastrService,
              private router: Router) {}
  
  ngOnInit(): void {
   this.listarCategorias();
  }

  listarCategorias() {
    this.categoriaService.obterTodos()
    .subscribe(
      categorias => this.categorias = categorias,
      erros => this.errorMessage
    )
  }

  abrirModal(id: string){
    this.categoriaId = id;
  }

  excluir(){
    this.categoriaService.excluir(this.categoriaId)
    .subscribe(
      sucesso => { this.processarSucesso() },
      erros => { this.processarFalha(erros) }
    );
   
  }

  processarSucesso() {
    Modal.fecharModal("modalExcluir");
    this.errors = [];

    this.toastr.success('Excluido com Sucesso!');
    this.listarCategorias();
  }

  processarFalha(fail: any) {
    Modal.fecharModal("modalExcluir");
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

}
