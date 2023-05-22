import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DetalhesUsuarioComponent } from './usuario/detalhes-usuario/detalhes-usuario.component';
import { NovoUsuarioComponent } from './usuario/novo-usuario/novo-usuario.component';
import { AdminLayoutComponent } from './navegacao/layout/admin-layout.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ListarUsuariosComponent } from './usuario/listar-usuarios/listar-usuarios.component';
import { EditarUsuarioComponent } from './usuario/editar-usuario/editar-usuario.component';
import { UsuarioResolver } from './services/usuarios/usuario.resolver';
import { ListarCategoriasComponent } from './categoria/listar-categorias/listar-categorias.component';
import { NovaCategoriaComponent } from './categoria/nova-categoria/nova-categoria.component';
import { EditarCategoriaComponent } from './categoria/editar-categoria/editar-categoria.component';
import { CategoriaResolver } from './services/categorias/categoria.resolver';
import { DetalhesCategoriaComponent } from './categoria/detalhes-categoria/detalhes-categoria.component';
import { ListarProvinciasComponent } from './provincia/listar-provincias/listar-provincias.component';
import { NovaProvinciaComponent } from './provincia/nova-provincia/nova-provincia.component';
import { EditarProvinciaComponent } from './provincia/editar-provincia/editar-provincia.component';
import { ProvinciaResolver } from './services/provincias/provincia.resolver';
import { DetalhesProvinciaComponent } from './provincia/detalhes-provincia/detalhes-provincia.component';
import { ListarMunicipiosComponent } from './municipio/listar-municipios/listar-municipios.component';
import { NovoMunicipioComponent } from './municipio/novo-municipio/novo-municipio.component';
import { EditarMunicipioComponent } from './municipio/editar-municipio/editar-municipio.component';
import { MunicipioResolver } from './services/provincias/municipio.resolver';
import { DetalhesMunicipioComponent } from './municipio/detalhes-municipio/detalhes-municipio.component';
import { ListarUnidadesComponent } from './unidade/listar-unidades/listar-unidades.component';
import { NovaUnidadeComponent } from './unidade/nova-unidade/nova-unidade.component';
import { EditarUnidadeComponent } from './unidade/editar-unidade/editar-unidade.component';
import { UnidadeResolver } from './services/unidades/unidade.resolver';
import { DetalhesUnidadeComponent } from './unidade/detalhes-unidade/detalhes-unidade.component';
import { ListarImpostosComponent } from './imposto/listar-impostos/listar-impostos.component';
import { NovoImpostoComponent } from './imposto/novo-imposto/novo-imposto.component';
import { EditarImpostoComponent } from './imposto/editar-imposto/editar-imposto.component';
import { ImpostoResolver } from './services/impostos/imposto.resolver';
import { DetalhesImpostoComponent } from './imposto/detalhes-imposto/detalhes-imposto.component';
import { ListarMotivosComponent } from './motivo/listar-motivos/listar-motivos.component';
import { NovoMotivoComponent } from './motivo/novo-motivo/novo-motivo.component';
import { EditarMotivoComponent } from './motivo/editar-motivo/editar-motivo.component';
import { MotivoResolver } from './services/impostos/motivo.resolver';
import { DetalhesMotivoComponent } from './motivo/detalhes-motivo/detalhes-motivo.component';
import { ProdutoResolver } from './services/produtos/produto.resolver';
import { DetalhesProdutoComponent } from './produto/detalhes-produto/detalhes-produto.component';
import { EditarProdutoComponent } from './produto/editar-produto/editar-produto.component';
import { NovoProdutoComponent } from './produto/novo-produto/novo-produto.component';
import { ListarProdutosComponent } from './produto/listar-produtos/listar-produtos.component';
import { ListarPacotesComponent } from './pacote/listar-pacotes/listar-pacotes.component';
import { NovoPacoteComponent } from './pacote/novo-pacote/novo-pacote.component';
import { EditarPacoteComponent } from './pacote/editar-pacote/editar-pacote.component';
import { PacoteResolver } from './services/pacotes/pacote.resolver';
import { DetalhesPacoteComponent } from './pacote/detalhes-pacote/detalhes-pacote.component';
import { AdminLoginComponent } from './admin-login/admin-login.component';
import { AdminGuard } from './guards/admin.guard';

const routes: Routes = [
  {
    path: '', component: AdminLayoutComponent,
    children: [
      {path: 'home', component: DashboardComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {path: 'usuarios', component: ListarUsuariosComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {path: 'novo-usuario', component: NovoUsuarioComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {
        path: 'editar-usuario/:id', component: EditarUsuarioComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          usuario: UsuarioResolver
        }
      },
      {
        path: 'detalhes-usuario/:id', component: DetalhesUsuarioComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          usuario: UsuarioResolver
        }
      },

      {path: 'categorias', component: ListarCategoriasComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {path: 'nova-categoria', component: NovaCategoriaComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {
        path: 'editar-categoria/:id', component: EditarCategoriaComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          categoria: CategoriaResolver
        }
      },
      {
        path: 'detalhes-categoria/:id', component: DetalhesCategoriaComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          categoria: CategoriaResolver
        }
      },

      {path: 'unidades', component: ListarUnidadesComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {path: 'nova-unidade', component: NovaUnidadeComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {
        path: 'editar-unidade/:id', component: EditarUnidadeComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          unidade: UnidadeResolver
        }
      },
      {
        path: 'detalhes-unidade/:id', component: DetalhesUnidadeComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          unidade: UnidadeResolver
        }
      },

      {path: 'provincias', component: ListarProvinciasComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {path: 'nova-provincia', component: NovaProvinciaComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {
        path: 'editar-provincia/:id', component: EditarProvinciaComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          provincia: ProvinciaResolver
        }
      },
      {
        path: 'detalhes-provincia/:id', component: DetalhesProvinciaComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          provincia: ProvinciaResolver
        }
      },

      {path: 'municipios', component: ListarMunicipiosComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {path: 'novo-municipio', component: NovoMunicipioComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {
        path: 'editar-municipio/:id', component: EditarMunicipioComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          municipio: MunicipioResolver
        }
      },
      {
        path: 'detalhes-municipio/:id', component: DetalhesMunicipioComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          municipio: MunicipioResolver
        }
      },

      {path: 'impostos', component: ListarImpostosComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {path: 'novo-imposto', component: NovoImpostoComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {
        path: 'editar-imposto/:id', component: EditarImpostoComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          imposto: ImpostoResolver
        }
      },
      {
        path: 'detalhes-imposto/:id', component: DetalhesImpostoComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          imposto: ImpostoResolver
        }
      },

      {path: 'motivos', component: ListarMotivosComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {path: 'novo-motivo', component: NovoMotivoComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {
        path: 'editar-motivo/:id', component: EditarMotivoComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          motivo: MotivoResolver
        }
      },
      {
        path: 'detalhes-motivo/:id', component: DetalhesMotivoComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          motivo: MotivoResolver
        }
      },

      {path: 'produtos', component: ListarProdutosComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {path: 'novo-produto', component: NovoProdutoComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {
        path: 'editar-produto/:id', component: EditarProdutoComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          produto: ProdutoResolver
        }
      },
      {
        path: 'detalhes-produto/:id', component: DetalhesProdutoComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          produto: ProdutoResolver
        }
      },

      {path: 'pacotes', component: ListarPacotesComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {path: 'novo-pacote', component: NovoPacoteComponent, canActivate: [AdminGuard], data: {role: ['Admin']},},
      {
        path: 'editar-pacote/:id', component: EditarPacoteComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          pacote: PacoteResolver
        }
      },
      {
        path: 'detalhes-pacote/:id', component: DetalhesPacoteComponent,
        canActivate: [AdminGuard],
        data: {role: ['Admin']},
        resolve: {
          pacote: PacoteResolver
        }
      }
      
    ]
  },
  { path: 'login', component: AdminLoginComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
