import { Component } from '@angular/core';
import { Categoria } from '../../models/categorias/categoria';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CategoriaService } from '../../services/categorias/categoria.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detalhes-categoria',
  templateUrl: './detalhes-categoria.component.html'
})
export class DetalhesCategoriaComponent {
  categoriaForm: FormGroup;
  categoria: Categoria = new Categoria();

  constructor(private fb: FormBuilder,
              private categoriaService: CategoriaService,
              private route: ActivatedRoute) {
      
      this.categoria = this.route.snapshot.data['categoria'];
  }

  ngOnInit(): void {
    this.inicializarFormulario();
    this.preencherForm();
  }

  preencherForm() {
    this.categoriaForm.patchValue({
      descricao: this.categoria.descricao
    });

    this.categoriaForm.disable();
  }

  inicializarFormulario() {
    this.categoriaForm = this.fb.group({
      descricao: ['']
    });
  }
}
