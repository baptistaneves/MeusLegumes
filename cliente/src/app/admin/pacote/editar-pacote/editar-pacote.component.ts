import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, AbstractControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageTransform, ImageCroppedEvent, Dimensions } from 'ngx-image-cropper';
import { ToastrService } from 'ngx-toastr';
import { FormBaseComponent } from 'src/app/base-component/form-base.component';
import { Pacote } from '../../models/pacotes/pacote';
import { Unidade } from '../../models/unidades/unidade';
import { PacoteService } from '../../services/pacotes/pacote.service';
import { UnidadeService } from '../../services/unidades/unidade.service';

@Component({
  selector: 'app-editar-pacote',
  templateUrl: './editar-pacote.component.html'
})
export class EditarPacoteComponent extends FormBaseComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  imageChangedEvent: any = '';
  croppedImage: any = '';
  canvasRotation = 0;
  rotation = 0;
  scale = 1;
  showCropper = false;
  containWithinAspectRatio = false;
  transform: ImageTransform = {};
  imageURL: string;
  imagemNome: string;


  errors: any[] = [];
  unidades: Unidade[] = []
  pacoteForm: FormGroup;
  pacote: Pacote;

  constructor(private fb: FormBuilder,
              private pacoteService: PacoteService,
              private unidadeService: UnidadeService,
              private toastr: ToastrService,
              private router: Router,
              private route: ActivatedRoute) {
      
      super();

      this.validationMessages = {
        nome: {
          required: "Informe o nome"
        },
        unidadeId: {
          required: "Informe a unidade"
        },
        precoUnitario: {
          required: "Informe o preço"
        }
      };

      super.configurarMensagensValidacaoBase(this.validationMessages);
      this.pacote = this.route.snapshot.data['pacote'];
  }

  ngOnInit(): void {
    this.listarUnidades();
    this.inicializarFormulario();
    this.preencherFormulario();
  }

  listarUnidades() {
    this.unidadeService.obterTodos()
    .subscribe(
      unidades => this.unidades = unidades,
      erros => this.errors
    )
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.pacoteForm);
  }

  emPromocaoControl() : AbstractControl {
    return this.pacoteForm.get('emPromocao');
  }

  precoPromocionalControl() : AbstractControl {
    return this.pacoteForm.get('precoPromocional');
  }

  produtoEmPromocao() {
    if(this.emPromocaoControl().value === true) {
      this.precoPromocionalControl().disable();
    }
    else {
      this.precoPromocionalControl().enable();
    }
  }

  preencherFormulario() {
    this.pacoteForm.patchValue({
      unidadeId: this.pacote.unidadeId,
      nome: this.pacote.nome,
      descricao: this.pacote.descricao,
      precoUnitario: this.pacote.precoUnitario,
      emPromocao: this.pacote.emPromocao,
      precoPromocional: this.pacote.precoPromocional,
      activo: this.pacote.activo
    });
  }

  inicializarFormulario() {

    this.pacoteForm = this.fb.group({
      unidadeId: ['', [Validators.required]],
      nome: ['', [Validators.required]],
      descricao: [''],
      precoUnitario: ['', [Validators.required]],
      urlImagemPrincipal: [''],
      emPromocao: [false],
      precoPromocional: [''],
      activo: [true]
    });
  }

  editar() {
    if(this.pacoteForm.dirty && this.pacoteForm.valid) {
      let oldImage = this.pacote.urlImagemPrincipal;

      this.pacote = Object.assign({}, this.pacote, this.pacoteForm.value);

      if(this.croppedImage.split(',')[1]) {
        this.pacote.imagemUpload = this.croppedImage.split(',')[1];
        this.pacote.urlImagemPrincipal = this.imagemNome;
      }
      else {
        this.pacote.urlImagemPrincipal = oldImage;
      }
      
      this.pacoteService.editar(this.pacote)
            .subscribe(
              sucesso => { this.processarSucesso() },
              erros => { this.processarFalha(erros) }
            );
    }
  }

  processarSucesso() {
    this.pacoteForm.reset();
    this.errors = [];

    let toast = this.toastr.success('Alteração realizada com Sucesso!');
    if (toast) {
      toast.onHidden.subscribe(() => {
        this.router.navigate(['/admin/pacotes']);
      });
    }
  }

  processarFalha(fail: any) {
    this.errors = fail.error.data;
    this.toastr.error('Ocorreu um erro!', 'Opa :(');
  }

  fileChangeEvent(event: any): void {
    this.imageChangedEvent = event;
    this.imagemNome = event.currentTarget.files[0].name;
  }
  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
  }
  imageLoaded() {
    this.showCropper = true;
  }
  cropperReady(sourceImageDimensions: Dimensions) {
    console.log('Cropper ready', sourceImageDimensions);
  }
  loadImageFailed() {
    this.errors.push('O formato do arquivo ' + this.imagemNome + ' não é aceito.');
  }

}