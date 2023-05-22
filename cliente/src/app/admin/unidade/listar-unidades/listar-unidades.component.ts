import { Component } from '@angular/core';
import { UnidadeService } from '../../services/unidades/unidade.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Modal } from 'src/app/utils/modal';
import { Unidade } from '../../models/unidades/unidade';

@Component({
  selector: 'app-listar-unidades',
  templateUrl: './listar-unidades.component.html'
})
export class ListarUnidadesComponent {

  errors: any[] = [];
  unidades: Unidade[];
  unidadeId: string;
  errorMessage: string;

  constructor(private unidadeService: UnidadeService,
              private toastr: ToastrService,
              private router: Router) {}
  
  ngOnInit(): void {
   this.listarUnidades();
  }

  listarUnidades() {
    this.unidadeService.obterTodos()
    .subscribe(
      unidades => this.unidades = unidades,
      erros => this.errorMessage
    )
  }

  abrirModal(id: string){
    this.unidadeId = id;
  }

  excluir(){
    this.unidadeService.excluir(this.unidadeId)
    .subscribe(
      sucesso => { this.processarSucesso() },
      erros => { this.processarFalha(erros) }
    );
   
  }

  processarSucesso() {
    Modal.fecharModal("modalExcluir");
    this.errors = [];

    this.toastr.success('Excluido com Sucesso!');
    this.listarUnidades();
  }

  processarFalha(fail: any) {
    Modal.fecharModal("modalExcluir");
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

}
