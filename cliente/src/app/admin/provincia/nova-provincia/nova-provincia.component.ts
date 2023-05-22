import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControlName } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

import { FormBaseComponent } from 'src/app/base-component/form-base.component';
import { Provincia } from '../../models/provincias/provincia';
import { ProvinciaService } from '../../services/provincias/provincia.service';

@Component({
  selector: 'app-nova-provincia',
  templateUrl: './nova-provincia.component.html'
})
export class NovaProvinciaComponent extends FormBaseComponent implements OnInit, AfterViewInit {
  
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  cadastroForm: FormGroup;
  provincia: Provincia;

  constructor(private fb: FormBuilder,
              private provinciaService: ProvinciaService,
              private toastr: ToastrService,
              private router: Router) {
      
      super();

      this.validationMessages = {
        nome: {
          required: "Informe o nome"
        }
      };

      super.configurarMensagensValidacaoBase(this.validationMessages);
  }

  ngOnInit(): void {
    this.inicializarFormulario();
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.cadastroForm);
  }

  inicializarFormulario() {

    this.cadastroForm = this.fb.group({
      nome: ['', [Validators.required]]
    });
  }

  adicionar() {
    if(this.cadastroForm.dirty && this.cadastroForm.valid) {
      this.provincia = Object.assign({}, this.provincia, this.cadastroForm.value);

      this.provinciaService.adicionarProvincia(this.provincia)
            .subscribe(
              sucesso => { this.processarSucesso() },
              erros => { this.processarFalha(erros) }
            );
    }
  }

  processarSucesso() {
    this.cadastroForm.reset();
    this.errors = [];

    let toast = this.toastr.success('Cadastro realizado com Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/admin/provincias']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

}
