using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeusLegumes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TabelaPacoteProdutosAlteradoParaPacoteItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacotesProduto");

            migrationBuilder.CreateTable(
                name: "PacoteItems",
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
                    table.PrimaryKey("PK_PacoteItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PacoteItems_Pacotes_PacoteId",
                        column: x => x.PacoteId,
                        principalTable: "Pacotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PacoteItems_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PacoteItems_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PacoteItems_PacoteId",
                table: "PacoteItems",
                column: "PacoteId");

            migrationBuilder.CreateIndex(
                name: "IX_PacoteItems_ProdutoId",
                table: "PacoteItems",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_PacoteItems_UnidadeId",
                table: "PacoteItems",
                column: "UnidadeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PacoteItems");

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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
        }
    }
}
