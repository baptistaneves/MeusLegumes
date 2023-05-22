import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { Produto } from '../../models/produtos/produto';
import { ProdutoService } from '../../services/produtos/produto.service';
import { FormControlName, FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { FormBaseComponent } from 'src/app/base-component/form-base.component';
import { Categoria } from '../../models/categorias/categoria';
import { Unidade } from '../../models/unidades/unidade';
import { Imposto } from '../../models/impostos/imposto';
import { Motivo } from '../../models/impostos/motivo';
import { CategoriaService } from '../../services/categorias/categoria.service';
import { UnidadeService } from '../../services/unidades/unidade.service';
import { ImpostoService } from '../../services/impostos/imposto.service';
import { MotivoService } from '../../services/impostos/motivo.service';
import { ImageCroppedEvent, ImageTransform, Dimensions } from 'ngx-image-cropper';

@Component({
  selector: 'app-novo-produto',
  templateUrl: './novo-produto.component.html'
})
export class NovoProdutoComponent extends FormBaseComponent implements OnInit, AfterViewInit {

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
  categorias: Categoria[] = [];
  unidades: Unidade[] = [];
  impostos: Imposto[] = [];
  motivos: Motivo[] = [];
  cadastroForm: FormGroup;
  produto: Produto;

  constructor(private fb: FormBuilder,
              private produtoService: ProdutoService,
              private categoriaService: CategoriaService,
              private unidadeService: UnidadeService,
              private impostoService: ImpostoService,
              private motivoService: MotivoService,
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
        categoriaId: {
          required: "Informe a categoria"
        },
        unidadeId: {
          required: "Informe a unidades"
        },
        impostoId: {
          required: "Informe o imposto"
        },
        motivoId: {
          required: "Informe o  motivo"
        },
        precoUnitario: {
          required: "Informe o preço"
        }
      };

      super.configurarMensagensValidacaoBase(this.validationMessages);
  }

  listarCategorias() {
    this.categoriaService.obterTodos()
    .subscribe(
      categorias => this.categorias = categorias,
      erros => this.errors
    )
  }

  listarUnidades() {
    this.unidadeService.obterTodos()
    .subscribe(
      unidades => this.unidades = unidades,
      erros => this.errors
    )
  }

  listarImpostos() {
    this.impostoService.obterTodos()
    .subscribe(
      impostos => this.impostos = impostos,
      erros => this.errors
    )
  }

  listarMotivos() {
    this.motivoService.obterTodos()
    .subscribe(
      motivos => this.motivos = motivos,
      erros => this.errors
    )
  }

  ngOnInit(): void {
    this.listarCategorias();
    this.listarImpostos();
    this.listarMotivos();
    this.listarUnidades();

    this.inicializarFormulario();
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
      nome: ['', [Validators.required]],
      urlImagemPrincipal: ['', [Validators.required]],
      categoriaId: ['', [Validators.required]],
      unidadeId: ['', [Validators.required]],
      impostoId: ['', [Validators.required]],
      motivoId: ['', [Validators.required]],
      precoUnitario: ['', [Validators.required]],
      emPromocao: [false],
      precoPromocional: [''],
      destaque: [false],
      novoLancamento: [false],
      maisVendido: [false],
      maisProcurado: [false],
      emEstoque: [false],
      activo: [true],
      observacao: [''],     
      descricao: ['']      
    });
  }

  adicionar() {
    if(this.cadastroForm.dirty && this.cadastroForm.valid) {
      this.produto = Object.assign({}, this.produto, this.cadastroForm.value);
      
      this.produto.imagemUpload = this.croppedImage.split(',')[1];
      this.produto.urlImagemPrincipal = this.imagemNome;

      this.produtoService.adicionar(this.produto)
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
        this.router.navigate(['/admin/produtos']);
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