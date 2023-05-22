import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { Motivo } from '../../models/impostos/motivo';
import { MotivoService } from '../../services/impostos/motivo.service';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBaseComponent } from 'src/app/base-component/form-base.component';

@Component({
  selector: 'app-editar-motivo',
  templateUrl: './editar-motivo.component.html'
})
export class EditarMotivoComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  errors: any[] = [];
  motivoForm: FormGroup;
  motivo: Motivo = new Motivo();

  constructor(private fb: FormBuilder,
              private motivoService: MotivoService,
              private toastr: ToastrService,
              private router: Router,
              private route: ActivatedRoute) {
      
      super();

      this.validationMessages = {
        codigoInterno: {
          required: "Informe o código"
        },
        mencaoFactura: {
          required: "Informe a menção"
        },
        normaLegalAplicavel: {
          required: "Informe a norma"
        },
        motivo: {
          required: "Informe o motivo"
        }

      };

      super.configurarMensagensValidacaoBase(this.validationMessages);

      this.motivo = this.route.snapshot.data['motivo'];
  }

  ngOnInit(): void {
    this.inicializarFormulario();
    this.preencherForm();
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.motivoForm);
  }

  preencherForm() {
    this.motivoForm.patchValue({
      codigoInterno: this.motivo.codigoInterno,
      mencaoFactura: this.motivo.mencaoFactura,
      normaLegalAplicavel: this.motivo.normaLegalAplicavel,
      motivo: this.motivo.motivo
    });
  }

  inicializarFormulario() {
    this.motivoForm = this.fb.group({
      codigoInterno: ['', [Validators.required]],
      mencaoFactura: ['', [Validators.required]],
      normaLegalAplicavel: ['', [Validators.required]],
      motivo: ['', [Validators.required]]
    });
  }

  editar() {
    if(this.motivoForm.dirty && this.motivoForm.valid) {
      this.motivo = Object.assign({}, this.motivo, this.motivoForm.value);

      this.motivoService.editar(this.motivo)
      .subscribe(
        sucesso => { this.processarSucesso() },
        erros => { this.processarFalha(erros) }
      );
    }
  }

  processarSucesso() {
    this.errors = [];

    let toast = this.toastr.success('Motivo Alterado com Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/admin/motivos']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

}
