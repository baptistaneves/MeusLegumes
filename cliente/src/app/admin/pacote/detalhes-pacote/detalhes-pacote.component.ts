import { Component } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Pacote } from '../../models/pacotes/pacote';
import { PacoteService } from '../../services/pacotes/pacote.service';
import { UnidadeService } from '../../services/unidades/unidade.service';
import { Unidade } from '../../models/unidades/unidade';

@Component({
  selector: 'app-detalhes-pacote',
  templateUrl: './detalhes-pacote.component.html'
})
export class DetalhesPacoteComponent {

  pacoteForm: FormGroup;
  pacote: Pacote;

  unidades: Unidade[] = []


  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private unidadeService: UnidadeService,
              ) {

      this.pacote = this.route.snapshot.data['pacote'];
  }

  ngOnInit(): void {
    this.listarUnidades();
    
    this.inicializarFormulario();
    this.preencherFormulario();
  }

  listarUnidades() {
    this.unidadeService.obterTodos()
    .subscribe(
      unidades => this.unidades = unidades,
    )
  }

  preencherFormulario() {
    this.pacoteForm.patchValue({
      unidadeId: this.pacote.unidadeId,
      nome: this.pacote.nome,
      descricao: this.pacote.descricao,
      precoUnitario: this.pacote.precoUnitario,
      emPromocao: this.pacote.emPromocao,
      precoPromocional: this.pacote.precoPromocional,
      activo: this.pacote.activo
    });

    this.pacoteForm.disable();
  }

  inicializarFormulario() {

    this.pacoteForm = this.fb.group({
      unidadeId: [''],
      nome: [''],
      descricao: [''],
      precoUnitario: [''],
      urlImagemPrincipal: [''],
      emPromocao: [''],
      precoPromocional: [''],
      activo: ['']
    });
  }

}
