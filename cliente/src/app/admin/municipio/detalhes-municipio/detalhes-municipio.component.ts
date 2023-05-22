import { Component } from '@angular/core';
import { Municipio } from '../../models/provincias/municipio';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ProvinciaService } from '../../services/provincias/provincia.service';
import { Provincia } from '../../models/provincias/provincia';

@Component({
  selector: 'app-detalhes-municipio',
  templateUrl: './detalhes-municipio.component.html'
})
export class DetalhesMunicipioComponent {
  
  errors: any[] = [];
  municipioForm: FormGroup;
  municipio: Municipio = new Municipio();
  provincias: Provincia[];
  
  constructor(private fb: FormBuilder,
              private provinciaService: ProvinciaService,
              private route: ActivatedRoute) {
      
      this.municipio = this.route.snapshot.data['municipio'];
  }

  ngOnInit(): void {
    this.listarProvincias();
    this.inicializarFormulario();
    this.preencherForm();
  }

  
  listarProvincias() {
    this.provinciaService.obterProvincias()
    .subscribe(
      provincias => this.provincias = provincias,
      erros => this.errors
    )
  }

  preencherForm() {
    this.municipioForm.patchValue({
      nome: this.municipio.nome,
      provinciaId: this.municipio.provinciaId
    });

    this.municipioForm.disable();
  }

  inicializarFormulario() {
    this.municipioForm = this.fb.group({
      nome: [''],
      provinciaId: ['']
    });
  }

}
