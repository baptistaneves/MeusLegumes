import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, AbstractControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ImageTransform, ImageCroppedEvent, Dimensions } from 'ngx-image-cropper';
import { ToastrService } from 'ngx-toastr';

import { FormBaseComponent } from 'src/app/base-component/form-base.component';
import { Categoria } from '../../models/categorias/categoria';
import { Imposto } from '../../models/impostos/imposto';
import { Motivo } from '../../models/impostos/motivo';
import { Produto } from '../../models/produtos/produto';
import { Unidade } from '../../models/unidades/unidade';
import { CategoriaService } from '../../services/categorias/categoria.service';
import { ImpostoService } from '../../services/impostos/imposto.service';
import { MotivoService } from '../../services/impostos/motivo.service';
import { ProdutoService } from '../../services/produtos/produto.service';
import { UnidadeService } from '../../services/unidades/unidade.service';

@Component({
  selector: 'app-editar-produto',
  templateUrl: './editar-produto.component.html'
})
export class EditarProdutoComponent  extends FormBaseComponent implements OnInit, AfterViewInit {

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
  produtoForm: FormGroup;
  produto: Produto;

  constructor(private fb: FormBuilder,
              private produtoService: ProdutoService,
              private categoriaService: CategoriaService,
              private unidadeService: UnidadeService,
              private impostoService: ImpostoService,
              private motivoService: MotivoService,
              private toastr: ToastrService,
              private router: Router,
              private route: ActivatedRoute) {
      
      super();
      this.produto = this.route.snapshot.data['produto'];

      this.validationMessages = {
        nome: {
          required: "Informe o nome"
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
    this.preencherFormulario();
  }

  preencherFormulario() {
    this.produtoForm.patchValue({
      nome: this.produto.nome,
      categoriaId: this.produto.categoriaId,
      unidadeId: this.produto.unidadeId,
      impostoId: this.produto.impostoId,
      motivoId: this.produto.motivoId,
      precoUnitario: this.produto.precoUnitario,
      emPromocao: this.produto.emPromocao,
      precoPromocional: this.produto.precoPromocional,
      destaque: this.produto.destaque,
      novoLancamento: this.produto.novoLancamento,
      maisVendido: this.produto.maisVendido,
      maisProcurado: this.produto.maisProcurado,
      emEstoque: this.produto.emEstoque,
      activo: this.produto.activo,
      observacao: this.produto.observacao,     
      descricao: this.produto.descricao  
    });
  }

  ngAfterViewInit(): void {
    super.configurarValidacaoFormularioBase(this.formInputElements, this.produtoForm);
  }

  emPromocaoControl() : AbstractControl {
    return this.produtoForm.get('emPromocao');
  }

  precoPromocionalControl() : AbstractControl {
    return this.produtoForm.get('precoPromocional');
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

    this.produtoForm = this.fb.group({
      nome: ['', [Validators.required]],
      urlImagemPrincipal: [''],
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

  editar() {
    if(this.produtoForm.dirty && this.produtoForm.valid) {
      let oldImage = this.produto.urlImagemPrincipal;

      this.produto = Object.assign({}, this.produto, this.produtoForm.value);
      
      if(this.croppedImage.split(',')[1]) {
        this.produto.imagemUpload = this.croppedImage.split(',')[1];
        this.produto.urlImagemPrincipal = this.imagemNome;
      }
      else {
        this.produto.urlImagemPrincipal = oldImage;
      }

      this.produtoService.editar(this.produto)
            .subscribe(
              sucesso => { this.processarSucesso() },
              erros => { this.processarFalha(erros) }
            );
    }
  }

  processarSucesso() {
    this.produtoForm.reset();
    this.errors = [];

    let toast = this.toastr.success('Alteração realizada com Sucesso!');
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
