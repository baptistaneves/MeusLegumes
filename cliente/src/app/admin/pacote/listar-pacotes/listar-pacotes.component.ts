import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Modal } from 'src/app/utils/modal';
import { Pacote } from '../../models/pacotes/pacote';
import { PacoteService } from '../../services/pacotes/pacote.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-listar-pacotes',
  templateUrl: './listar-pacotes.component.html'
})
export class ListarPacotesComponent {

  imagens: string = environment.imagensUrl;

  errors: any[] = [];
  pacotes: Pacote[];
  pacoteId: string;
  errorMessage: string;

  constructor(private pacoteService: PacoteService,
              private toastr: ToastrService) {}
  
  ngOnInit(): void {
   this.listarPacotes();
  }

  listarPacotes() {
    this.pacoteService.obterTodos()
    .subscribe(
      pacotes => this.pacotes = pacotes,
      erros => this.errorMessage
    )
  }

  abrirModal(id: string){
    this.pacoteId = id;
  }

  excluir(){
    this.pacoteService.excluir(this.pacoteId)
    .subscribe(
      sucesso => { this.processarSucesso() },
      erros => { this.processarFalha(erros) }
    );
   
  }

  processarSucesso() {
    Modal.fecharModal("modalExcluir");
    this.errors = [];

    this.toastr.success('Excluido com Sucesso!');
    this.listarPacotes();
  }

  processarFalha(fail: any) {
    Modal.fecharModal("modalExcluir");
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

}
