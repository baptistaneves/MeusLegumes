import { Component } from '@angular/core';
import { Imposto } from '../../models/impostos/imposto';
import { ImpostoService } from '../../services/impostos/imposto.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Modal } from 'src/app/utils/modal';

@Component({
  selector: 'app-listar-impostos',
  templateUrl: './listar-impostos.component.html'
})
export class ListarImpostosComponent {

  errors: any[] = [];
  impostos: Imposto[];
  impostoId: string;
  errorMessage: string;

  constructor(private impostoService: ImpostoService,
              private toastr: ToastrService,
              private router: Router) {}
  
  ngOnInit(): void {
   this.listarImpostos();
  }

  listarImpostos() {
    this.impostoService.obterTodos()
    .subscribe(
      impostos => this.impostos = impostos,
      erros => this.errorMessage
    )
  }

  abrirModal(id: string){
    this.impostoId = id;
  }

  excluir(){
    this.impostoService.excluir(this.impostoId)
    .subscribe(
      sucesso => { this.processarSucesso() },
      erros => { this.processarFalha(erros) }
    );
   
  }

  processarSucesso() {
    Modal.fecharModal("modalExcluir");
    this.errors = [];

    this.toastr.success('Excluido com Sucesso!');
    this.listarImpostos();
  }

  processarFalha(fail: any) {
    Modal.fecharModal("modalExcluir");
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

}
