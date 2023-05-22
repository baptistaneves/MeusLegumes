import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { ProvinciaService } from '../../services/provincias/provincia.service';
import { Modal } from 'src/app/utils/modal';
import { Provincia } from '../../models/provincias/provincia';

@Component({
  selector: 'app-listar-provincias',
  templateUrl: './listar-provincias.component.html'
})
export class ListarProvinciasComponent {

  errors: any[] = [];
  provincias: Provincia[];
  provinciaId: string;
  errorMessage: string;

  constructor(private provinciaService: ProvinciaService,
              private toastr: ToastrService,
              private router: Router) {}
  
  ngOnInit(): void {
   this.listarProvincias();
  }

  listarProvincias() {
    this.provinciaService.obterProvincias()
    .subscribe(
      provincias => this.provincias = provincias,
      erros => this.errorMessage
    )
  }

  abrirModal(id: string){
    this.provinciaId = id;
  }

  excluir(){
    this.provinciaService.excluirProvincia(this.provinciaId)
    .subscribe(
      sucesso => { this.processarSucesso() },
      erros => { this.processarFalha(erros) }
    );
   
  }

  processarSucesso() {
    Modal.fecharModal("modalExcluir");
    this.errors = [];

    this.toastr.success('Excluido com Sucesso!');
    this.listarProvincias();
  }

  processarFalha(fail: any) {
    Modal.fecharModal("modalExcluir");
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

}
