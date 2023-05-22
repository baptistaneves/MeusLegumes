import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { Pacote } from '../../models/pacotes/pacote';
import { PacoteService } from '../../services/pacotes/pacote.service';
import { FormControlName, FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBaseComponent } from 'src/app/base-component/form-base.component';
import { UnidadeService } from '../../services/unidades/unidade.service';
import { Unidade } from '../../models/unidades/unidade';

import { ImageCroppedEvent, ImageTransform, Dimensions } from 'ngx-image-cropper';


@Component({
  selector: 'app-novo-pacote',
  templateUrl: './novo-pacote.component.html'
})
export class NovoPacoteComponent extends FormBaseComponent implements OnInit, AfterViewInit {

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
  cadastroForm: FormGroup;
  pacote: Pacote;

  constructor(private fb: FormBuilder,
              private pacoteService: PacoteService,
              private unidadeService: UnidadeService,
              private toastr: ToastrService,
              private router: Router) {
      
      super();

      this.validationMessages = {
        nome: {
          required: "Informe o nome"
        },
        urlImagemPrincipal: {
          required: "Selecione uma imagem"
        },
        unidadeId: {
          required: "Informe a unidade"
        },
        precoUnitario: {
          required: "Informe o preço"
        }
      };

      super.configurarMensagensValidacaoBase(this.validationMessages);
  }

  ngOnInit(): void {
    this.listarUnidades();
    this.inicializarFormulario();
  }

  listarUnidades() {
    this.unidadeService.obterTodos()
    .subscribe(
      unidades => this.unidades = unidades,
      erros => this.errors
    )
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.cadastroForm);
  }

  emPromocaoControl() : AbstractControl {
    return this.cadastroForm.get('emPromocao');
  }

  precoPromocionalControl() : AbstractControl {
    return this.cadastroForm.get('precoPromocional');
  }

  produtoEmPromocao() {
    if(this.emPromocaoControl().value === true) {
      this.precoPromocionalControl().disable();
    }
    else {
      this.precoPromocionalControl().enable();
    }
  }

  inicializarFormulario() {

    this.cadastroForm = this.fb.group({
      unidadeId: ['', [Validators.required]],
      nome: ['', [Validators.required]],
      descricao: [''],
      precoUnitario: ['', [Validators.required]],
      urlImagemPrincipal: ['', [Validators.required]],
      emPromocao: [false],
      precoPromocional: [''],
      activo: [true]
    });
  }

  adicionar() {
    if(this.cadastroForm.dirty && this.cadastroForm.valid) {
      this.pacote = Object.assign({}, this.pacote, this.cadastroForm.value);

      this.pacote.imagemUpload = this.croppedImage.split(',')[1];
      this.pacote.urlImagemPrincipal = this.imagemNome;

      this.pacoteService.adicionar(this.pacote)
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