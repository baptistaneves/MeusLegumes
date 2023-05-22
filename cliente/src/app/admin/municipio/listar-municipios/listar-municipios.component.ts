import { Router } from '@angular/router';
import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

import { Modal } from 'src/app/utils/modal';
import { ProvinciaService } from '../../services/provincias/provincia.service';
import { Municipio } from './../../models/provincias/municipio';

@Component({
  selector: 'app-listar-municipios',
  templateUrl: './listar-municipios.component.html'
})
export class ListarMunicipiosComponent {

  errors: any[] = [];
  municipios: Municipio[];
  municipioId: string;
  errorMessage: string;

  constructor(private provinciaService: ProvinciaService,
              private toastr: ToastrService,
              private router: Router) {}
  
  ngOnInit(): void {
   this.listarProvincias();
  }

  listarProvincias() {
    this.provinciaService.obterMunicipios()
    .subscribe(
      municipios => this.municipios = municipios,
      erros => this.errorMessage
    )
  }

  abrirModal(id: string){
    this.municipioId = id;
  }

  excluir(){
    this.provinciaService.excluirMunicipio(this.municipioId)
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
