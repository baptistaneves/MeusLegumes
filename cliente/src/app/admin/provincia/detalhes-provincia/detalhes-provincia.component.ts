import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Provincia } from '../../models/provincias/provincia';
import { ProvinciaService } from '../../services/provincias/provincia.service';
import { FormGroup, FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-detalhes-provincia',
  templateUrl: './detalhes-provincia.component.html'
})
export class DetalhesProvinciaComponent {

  provinciaForm: FormGroup;
  provincia: Provincia = new Provincia();

  constructor(private fb: FormBuilder,
              private provinciaService: ProvinciaService,
              private route: ActivatedRoute) {
      
      this.provincia = this.route.snapshot.data['provincia'];
  }

  ngOnInit(): void {
    this.inicializarFormulario();
    this.preencherForm();
  }

  preencherForm() {
    this.provinciaForm.patchValue({
      nome: this.provincia.nome
    });

    this.provinciaForm.disable();
  }

  inicializarFormulario() {
    this.provinciaForm = this.fb.group({
      nome: ['']
    });
  }

}
