using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeusLegumes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Impostos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(200)", nullable: false),
                    Taxa = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TipoDeTaxa = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Impostos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotivosIsencaoIva",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CodigoInterno = table.Column<string>(type: "varchar(100)", nullable: false),
                    MencaoFactura = table.Column<string>(type: "varchar(100)", nullable: false),
                    NormaLegalAplicavel = table.Column<string>(type: "varchar(100)", nullable: false),
                    Motivo = table.Column<string>(type: "varchar(500)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivosIsencaoIva", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pacotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EmPromocao = table.Column<bool>(type: "bit", nullable: false),
                    PrecoPromocional = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImagemUrl = table.Column<string>(type: "varchar(255)", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pacotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Provincias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provincias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(150)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Municipios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProvinciaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipios_Provincias_ProvinciaId",
                        column: x => x.ProvinciaId,
                        principalTable: "Provincias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImpostoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MotivoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UrlImagemPrincipal = table.Column<string>(type: "varchar(255)", nullable: false),
                    EmPromocao = table.Column<bool>(type: "bit", nullable: false),
                    PrecoPromocional = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Destaque = table.Column<bool>(type: "bit", nullable: false),
                    NovoLancamento = table.Column<bool>(type: "bit", nullable: false),
                    MaisVendido = table.Column<bool>(type: "bit", nullable: false),
                    MaisProcurado = table.Column<bool>(type: "bit", nullable: false),
                    EmEstoque = table.Column<bool>(type: "bit", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Observacao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produtos_Impostos_ImpostoId",
                        column: x => x.ImpostoId,
                        principalTable: "Impostos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produtos_MotivosIsencaoIva_MotivoId",
                        column: x => x.MotivoId,
                        principalTable: "MotivosIsencaoIva",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Produtos_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MunicipioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserIdentityId = table.Column<string>(type: "varchar(50)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(500)", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(50)", nullable: false),
                    NumeroContribuinte = table.Column<string>(type: "varchar(20)", nullable: false),
                    TelefonePrincipal = table.Column<string>(type: "varchar(20)", nullable: false),
                    TelefoneAlternativo = table.Column<string>(type: "varchar(20)", nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", nullable: false),
                    Rua = table.Column<string>(type: "varchar(255)", nullable: false),
                    Casa = table.Column<string>(type: "varchar(20)", nullable: false),
                    CodigoPostal = table.Column<string>(type: "varchar(200)", nullable: false),
                    PontoDeReferencia = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Municipios_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "Municipios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PacotesProduto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PacoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PacotesProduto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PacotesProduto_Pacotes_PacoteId",
                        column: x => x.PacoteId,
                        principalTable: "Pacotes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PacotesProduto_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PacotesProduto_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProdutosImagem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UrlImagem = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosImagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutosImagem_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProdutosRelacionado",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProdutoRelacionadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosRelacionado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutosRelacionado_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_MunicipioId",
                table: "Clientes",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipios_ProvinciaId",
                table: "Municipios",
                column: "ProvinciaId");

            migrationBuilder.CreateIndex(
                name: "IX_PacotesProduto_PacoteId",
                table: "PacotesProduto",
                column: "PacoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PacotesProduto_ProdutoId",
                table: "PacotesProduto",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_PacotesProduto_UnidadeId",
                table: "PacotesProduto",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_ImpostoId",
                table: "Produtos",
                column: "ImpostoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_MotivoId",
                table: "Produtos",
                column: "MotivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_UnidadeId",
                table: "Produtos",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosImagem_ProdutoId",
                table: "ProdutosImagem",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutosRelacionado_ProdutoId",
                table: "ProdutosRelacionado",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "PacotesProduto");

            migrationBuilder.DropTable(
                name: "ProdutosImagem");

            migrationBuilder.DropTable(
                name: "ProdutosRelacionado");

            migrationBuilder.DropTable(
                name: "Municipios");

            migrationBuilder.DropTable(
                name: "Pacotes");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Provincias");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Impostos");

            migrationBuilder.DropTable(
                name: "MotivosIsencaoIva");

            migrationBuilder.DropTable(
                name: "Unidades");
        }
    }
}
