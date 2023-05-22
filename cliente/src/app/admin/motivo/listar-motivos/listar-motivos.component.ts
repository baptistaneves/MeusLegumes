import { Component } from '@angular/core';
import { Motivo } from '../../models/impostos/motivo';
import { MotivoService } from '../../services/impostos/motivo.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Modal } from 'src/app/utils/modal';

@Component({
  selector: 'app-listar-motivos',
  templateUrl: './listar-motivos.component.html'
})
export class ListarMotivosComponent {

  errors: any[] = [];
  motivos: Motivo[];
  motivoId: string;
  errorMessage: string;

  constructor(private motivoService: MotivoService,
              private toastr: ToastrService,
              private router: Router) {}
  
  ngOnInit(): void {
   this.listarMotivos();
  }

  listarMotivos() {
    this.motivoService.obterTodos()
    .subscribe(
      motivos => this.motivos = motivos,
      erros => this.errorMessage
    )
  }

  abrirModal(id: string){
    this.motivoId = id;
  }

  excluir(){
    this.motivoService.excluir(this.motivoId)
    .subscribe(
      sucesso => { this.processarSucesso() },
      erros => { this.processarFalha(erros) }
    );
   
  }

  processarSucesso() {
    Modal.fecharModal("modalExcluir");
    this.errors = [];

    this.toastr.success('Excluido com Sucesso!');
    this.listarMotivos();
  }

  processarFalha(fail: any) {
    Modal.fecharModal("modalExcluir");
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

}
