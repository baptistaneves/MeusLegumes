import { Component } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Produto } from '../../models/produtos/produto';
import { CategoriaService } from '../../services/categorias/categoria.service';
import { ImpostoService } from '../../services/impostos/imposto.service';
import { MotivoService } from '../../services/impostos/motivo.service';
import { UnidadeService } from '../../services/unidades/unidade.service';
import { Categoria } from '../../models/categorias/categoria';
import { Imposto } from '../../models/impostos/imposto';
import { Motivo } from '../../models/impostos/motivo';
import { Unidade } from '../../models/unidades/unidade';

@Component({
  selector: 'app-detalhes-produto',
  templateUrl: './detalhes-produto.component.html'
})
export class DetalhesProdutoComponent {

  produtoForm: FormGroup;
  produto: Produto;

  categorias: Categoria[] = [];
  unidades: Unidade[] = [];
  impostos: Imposto[] = [];
  motivos: Motivo[] = [];

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute,
              private categoriaService: CategoriaService,
              private unidadeService: UnidadeService,
              private impostoService: ImpostoService,
              private motivoService: MotivoService,) {

      this.produto = this.route.snapshot.data['produto'];
  }


  ngOnInit(): void {
    this.listarCategorias();
    this.listarImpostos();
    this.listarMotivos();
    this.listarUnidades();

    this.inicializarFormulario();
    this.preencherFormulario();
  }

  listarCategorias() {
    this.categoriaService.obterTodos()
    .subscribe(
      categorias => this.categorias = categorias,
    )
  }

  listarUnidades() {
    this.unidadeService.obterTodos()
    .subscribe(
      unidades => this.unidades = unidades,
    )
  }

  listarImpostos() {
    this.impostoService.obterTodos()
    .subscribe(
      impostos => this.impostos = impostos,
    )
  }

  listarMotivos() {
    this.motivoService.obterTodos()
    .subscribe(
      motivos => this.motivos = motivos,
    )
  }

  preencherFormulario() {
    this.produtoForm.patchValue({
      nome: this.produto.nome,
      categoriaId: this.produto.categoriaId,
      unidadeId: this.produto.unidadeId,
      impostoId: this.produto.impostoId,
      motivoId: this.produto.motivoId,
      precoUnitario: this.produto.precoUnitario,
      emPromocao: this.produto.emPromocao,
      precoPromocional: this.produto.precoPromocional,
      destaque: this.produto.destaque,
      novoLancamento: this.produto.novoLancamento,
      maisVendido: this.produto.maisVendido,
      maisProcurado: this.produto.maisProcurado,
      emEstoque: this.produto.emEstoque,
      activo: this.produto.activo,
      observacao: this.produto.observacao,     
      descricao: this.produto.descricao  
    });

    this.produtoForm.disable();
  }

  inicializarFormulario() {

    this.produtoForm = this.fb.group({
      nome: [''],
      urlImagemPrincipal: [''],
      categoriaId: [''],
      unidadeId: [''],
      impostoId: [''],
      motivoId: [''],
      precoUnitario: [''],
      emPromocao: [''],
      precoPromocional: [''],
      destaque: [''],
      novoLancamento: [''],
      maisVendido: [''],
      maisProcurado: [''],
      emEstoque: [''],
      activo: [''],
      observacao: [''],     
      descricao: ['']      
    });
  }

}
