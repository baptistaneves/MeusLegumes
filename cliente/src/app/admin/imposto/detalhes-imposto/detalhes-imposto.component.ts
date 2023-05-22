import { Component } from '@angular/core';
import { Imposto } from '../../models/impostos/imposto';
import { ImpostoService } from '../../services/impostos/imposto.service';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detalhes-imposto',
  templateUrl: './detalhes-imposto.component.html'
})
export class DetalhesImpostoComponent {

  impostoForm: FormGroup;
  imposto: Imposto = new Imposto();

  constructor(private fb: FormBuilder,
              private impostoService: ImpostoService,
              private route: ActivatedRoute) {
      
      this.imposto = this.route.snapshot.data['imposto'];
  }

  ngOnInit(): void {
    this.inicializarFormulario();
    this.preencherForm();
  }

  preencherForm() {
    this.impostoForm.patchValue({
      descricao: this.imposto.descricao,
      taxa: this.imposto.taxa,
      tipoDeTaxa: this.imposto.tipoDeTaxa
    });

    this.impostoForm.disable();
  }

  inicializarFormulario() {
    this.impostoForm = this.fb.group({
      descricao: [''],
      taxa: [''],
      tipoDeTaxa: ['']
    });
  }

}
