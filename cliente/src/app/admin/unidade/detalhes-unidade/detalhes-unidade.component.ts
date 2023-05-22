import { Component } from '@angular/core';
import { Unidade } from '../../models/unidades/unidade';
import { UnidadeService } from '../../services/unidades/unidade.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detalhes-unidade',
  templateUrl: './detalhes-unidade.component.html'
})
export class DetalhesUnidadeComponent {

  unidadeForm: FormGroup;
  unidade: Unidade = new Unidade();

  constructor(private fb: FormBuilder,
              private unidadeService: UnidadeService,
              private route: ActivatedRoute) {
      
      this.unidade = this.route.snapshot.data['unidade'];
  }

  ngOnInit(): void {
    this.inicializarFormulario();
    this.preencherForm();
  }

  preencherForm() {
    this.unidadeForm.patchValue({
      descricao: this.unidade.descricao
    });

    this.unidadeForm.disable();
  }

  inicializarFormulario() {
    this.unidadeForm = this.fb.group({
      descricao: ['']
    });
  }

}
