import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { Unidade } from '../../models/unidades/unidade';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBaseComponent } from 'src/app/base-component/form-base.component';
import { CategoriaService } from '../../services/categorias/categoria.service';
import { UnidadeService } from '../../services/unidades/unidade.service';

@Component({
  selector: 'app-editar-unidade',
  templateUrl: './editar-unidade.component.html'
})
export class EditarUnidadeComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  unidadeForm: FormGroup;
  unidade: Unidade = new Unidade();

  constructor(private fb: FormBuilder,
              private unidadeService: UnidadeService,
              private toastr: ToastrService,
              private router: Router,
              private route: ActivatedRoute) {
      
      super();

      this.validationMessages = {
        descricao: {
          required: "Informe a descrição"
        }
      };

      super.configurarMensagensValidacaoBase(this.validationMessages);

      this.unidade = this.route.snapshot.data['unidade'];
  }

  ngOnInit(): void {
    this.inicializarFormulario();
    this.preencherForm();
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.unidadeForm);
  }

  preencherForm() {
    this.unidadeForm.patchValue({
      descricao: this.unidade.descricao,
    });
  }

  inicializarFormulario() {
    this.unidadeForm = this.fb.group({
      descricao: ['', [Validators.required]]
    });
  }

  editar() {
    if(this.unidadeForm.dirty && this.unidadeForm.valid) {
      this.unidade = Object.assign({}, this.unidade, this.unidadeForm.value);

      this.unidadeService.editar(this.unidade)
      .subscribe(
        sucesso => { this.processarSucesso() },
        erros => { this.processarFalha(erros) }
      );
    }
  }

  processarSucesso() {
    this.errors = [];

    let toast = this.toastr.success('Unidade Alterada com Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/admin/unidades']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

}
