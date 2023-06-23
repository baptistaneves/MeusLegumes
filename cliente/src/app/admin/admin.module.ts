import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ReactiveFormsModule } from '@angular/forms';


import { AdminRoutingModule } from './admin-routing.module';
import { AdminLayoutComponent } from './navegacao/layout/admin-layout.component';
import { AdminFooterComponent } from './navegacao/footer/admin-footer.component';
import { AdminMenuComponent } from './navegacao/menu/admin-menu.component';
import { AdminHeaderComponent } from './navegacao/header/admin-header.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UsuarioService } from './services/usuarios/usuario.service';
import { NarikCustomValidatorsModule } from '@narik/custom-validators';
import { ListarUsuariosComponent } from './usuario/listar-usuarios/listar-usuarios.component';
import { NovoUsuarioComponent } from './usuario/novo-usuario/novo-usuario.component';
import { EditarUsuarioComponent } from './usuario/editar-usuario/editar-usuario.component';
import { DetalhesUsuarioComponent } from './usuario/detalhes-usuario/detalhes-usuario.component';
import { UsuarioResolver } from './services/usuarios/usuario.resolver';
import { ListarCategoriasComponent } from './categoria/listar-categorias/listar-categorias.component';
import { NovaCategoriaComponent } from './categoria/nova-categoria/nova-categoria.component';
import { EditarCategoriaComponent } from './categoria/editar-categoria/editar-categoria.component';
import { DetalhesCategoriaComponent } from './categoria/detalhes-categoria/detalhes-categoria.component';
import { CategoriaService } from './services/categorias/categoria.service';
import { CategoriaResolver } from './services/categorias/categoria.resolver';
import { ListarProvinciasComponent } from './provincia/listar-provincias/listar-provincias.component';
import { NovaProvinciaComponent } from './provincia/nova-provincia/nova-provincia.component';
import { EditarProvinciaComponent } from './provincia/editar-provincia/editar-provincia.component';
import { DetalhesProvinciaComponent } from './provincia/detalhes-provincia/detalhes-provincia.component';
import { ListarMunicipiosComponent } from './municipio/listar-municipios/listar-municipios.component';
import { NovoMunicipioComponent } from './municipio/novo-municipio/novo-municipio.component';
import { EditarMunicipioComponent } from './municipio/editar-municipio/editar-municipio.component';
import { DetalhesMunicipioComponent } from './municipio/detalhes-municipio/detalhes-municipio.component';
import { ProvinciaService } from './services/provincias/provincia.service';
import { ProvinciaResolver } from './services/provincias/provincia.resolver';
import { MunicipioResolver } from './services/provincias/municipio.resolver';
import { ListarUnidadesComponent } from './unidade/listar-unidades/listar-unidades.component';
import { NovaUnidadeComponent } from './unidade/nova-unidade/nova-unidade.component';
import { EditarUnidadeComponent } from './unidade/editar-unidade/editar-unidade.component';
import { DetalhesUnidadeComponent } from './unidade/detalhes-unidade/detalhes-unidade.component';
import { UnidadeService } from './services/unidades/unidade.service';
import { UnidadeResolver } from './services/unidades/unidade.resolver';
import { ListarImpostosComponent } from './imposto/listar-impostos/listar-impostos.component';
import { NovoImpostoComponent } from './imposto/novo-imposto/novo-imposto.component';
import { EditarImpostoComponent } from './imposto/editar-imposto/editar-imposto.component';
import { DetalhesImpostoComponent } from './imposto/detalhes-imposto/detalhes-imposto.component';
import { ListarMotivosComponent } from './motivo/listar-motivos/listar-motivos.component';
import { NovoMotivoComponent } from './motivo/novo-motivo/novo-motivo.component';
import { EditarMotivoComponent } from './motivo/editar-motivo/editar-motivo.component';
import { DetalhesMotivoComponent } from './motivo/detalhes-motivo/detalhes-motivo.component';
import { ImpostoService } from './services/impostos/imposto.service';
import { MotivoService } from './services/impostos/motivo.service';
import { ImpostoResolver } from './services/impostos/imposto.resolver';
import { MotivoResolver } from './services/impostos/motivo.resolver';
import { ListarProdutosComponent } from './produto/listar-produtos/listar-produtos.component';
import { NovoProdutoComponent } from './produto/novo-produto/novo-produto.component';
import { EditarProdutoComponent } from './produto/editar-produto/editar-produto.component';
import { DetalhesProdutoComponent } from './produto/detalhes-produto/detalhes-produto.component';
import { ListarPacotesComponent } from './pacote/listar-pacotes/listar-pacotes.component';
import { NovoPacoteComponent } from './pacote/novo-pacote/novo-pacote.component';
import { EditarPacoteComponent } from './pacote/editar-pacote/editar-pacote.component';
import { DetalhesPacoteComponent } from './pacote/detalhes-pacote/detalhes-pacote.component';
import { ProdutoService } from './services/produtos/produto.service';
import { ProdutoResolver } from './services/produtos/produto.resolver';
import { PacoteService } from './services/pacotes/pacote.service';
import { PacoteResolver } from './services/pacotes/pacote.resolver';
import { ImageCropperModule } from 'ngx-image-cropper';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { AdminGuard } from './guards/admin.guard';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { ErrorInterceptor } from '../services/error.interceptor';

@NgModule({
  declarations: [
    AdminLayoutComponent,
    AdminFooterComponent,
    AdminMenuComponent,
    AdminHeaderComponent,
    DashboardComponent,
    NovoUsuarioComponent,
    ListarUsuariosComponent,
    EditarUsuarioComponent,
    DetalhesUsuarioComponent,
    ListarCategoriasComponent,
    NovaCategoriaComponent,
    EditarCategoriaComponent,
    DetalhesCategoriaComponent,
    ListarProvinciasComponent,
    NovaProvinciaComponent,
    EditarProvinciaComponent,
    DetalhesProvinciaComponent,
    ListarMunicipiosComponent,
    NovoMunicipioComponent,
    EditarMunicipioComponent,
    DetalhesMunicipioComponent,
    ListarUnidadesComponent,
    NovaUnidadeComponent,
    EditarUnidadeComponent,
    DetalhesUnidadeComponent,
    ListarImpostosComponent,
    NovoImpostoComponent,
    EditarImpostoComponent,
    DetalhesImpostoComponent,
    ListarMotivosComponent,
    NovoMotivoComponent,
    EditarMotivoComponent,
    DetalhesMotivoComponent,
    ListarProdutosComponent,
    NovoProdutoComponent,
    EditarProdutoComponent,
    DetalhesProdutoComponent,
    ListarPacotesComponent,
    NovoPacoteComponent,
    EditarPacoteComponent,
    DetalhesPacoteComponent,
    AdminLoginComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    ReactiveFormsModule,
    NarikCustomValidatorsModule,
    ImageCropperModule
  ],
  providers: [
    UsuarioService,
    UsuarioResolver,
    CategoriaService,
    CategoriaResolver,
    ProvinciaService,
    ProvinciaResolver,
    MunicipioResolver,
    UnidadeService,
    UnidadeResolver,
    ImpostoService,
    MotivoService,
    ImpostoResolver,
    MotivoResolver,
    ProdutoService,
    ProdutoResolver,
    PacoteService,
    PacoteResolver,
    AdminGuard
  ]
})
export class AdminModule { }
