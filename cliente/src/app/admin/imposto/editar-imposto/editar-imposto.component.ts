import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { ImpostoService } from '../../services/impostos/imposto.service';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { CustomValidators } from '@narik/custom-validators';
import { ToastrService } from 'ngx-toastr';
import { FormBaseComponent } from 'src/app/base-component/form-base.component';
import { Imposto } from '../../models/impostos/imposto';

@Component({
  selector: 'app-editar-imposto',
  templateUrl: './editar-imposto.component.html'
})
export class EditarImpostoComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  impostoForm: FormGroup;
  imposto: Imposto = new Imposto();

  constructor(private fb: FormBuilder,
              private impostoService: ImpostoService,
              private toastr: ToastrService,
              private router: Router,
              private route: ActivatedRoute) {
      
      super();

      this.validationMessages = {
        descricao: {
          required: "Informe a descrição"
        },
        taxa: {
          required: "Informe a taxa",
          number: "Informe somente números"
        },
        tipoDeTaxa: {
          required: "Informe o tipo de taxa"
        }
      };

      super.configurarMensagensValidacaoBase(this.validationMessages);

      this.imposto = this.route.snapshot.data['imposto'];
  }


  ngOnInit(): void {
    this.inicializarFormulario();
    this.preencherForm();
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.impostoForm);
  }

  preencherForm() {
    this.impostoForm.patchValue({
      descricao: this.imposto.descricao,
      taxa: this.imposto.taxa,
      tipoDeTaxa: this.imposto.tipoDeTaxa
    });
  }

  inicializarFormulario() {
    this.impostoForm = this.fb.group({
      descricao: ['', [Validators.required]],
      taxa: ['', [Validators.required, CustomValidators.number]],
      tipoDeTaxa: ['', [Validators.required]]
    });
  }

  editar() {
    if(this.impostoForm.dirty && this.impostoForm.valid) {
      this.imposto = Object.assign({}, this.imposto, this.impostoForm.value);

      this.impostoService.editar(this.imposto)
      .subscribe(
        sucesso => { this.processarSucesso() },
        erros => { this.processarFalha(erros) }
      );
    }
  }

  processarSucesso() {
    this.errors = [];

    let toast = this.toastr.success('Imposto Alterado com Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/admin/impostos']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

} 

