import { Component } from '@angular/core';
import { Motivo } from '../../models/impostos/motivo';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-detalhes-motivo',
  templateUrl: './detalhes-motivo.component.html'
})
export class DetalhesMotivoComponent {

  motivoForm: FormGroup;
  motivo: Motivo = new Motivo();

  constructor(private fb: FormBuilder,
              private route: ActivatedRoute) {
      
      this.motivo = this.route.snapshot.data['motivo'];
  }

  ngOnInit(): void {
    this.inicializarFormulario();
    this.preencherForm();
  }

  preencherForm() {
    this.motivoForm.patchValue({
      codigoInterno: this.motivo.codigoInterno,
      mencaoFactura: this.motivo.mencaoFactura,
      normaLegalAplicavel: this.motivo.normaLegalAplicavel,
      motivo: this.motivo.motivo
    });

    this.motivoForm.disable();
  }

  inicializarFormulario() {
    this.motivoForm = this.fb.group({
      codigoInterno: [''],
      mencaoFactura: [''],
      normaLegalAplicavel: [''],
      motivo: ['']
    });
  }

}
