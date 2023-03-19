using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeusLegumes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCascadeEClientSetNullDefinidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PacotesProduto_Pacotes_PacoteId",
                table: "PacotesProduto");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoImagens_Produtos_ProdutoId",
                table: "ProdutoImagens");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoRelacionados_Produtos_ProdutoId",
                table: "ProdutoRelacionados");

            migrationBuilder.AddForeignKey(
                name: "FK_PacotesProduto_Pacotes_PacoteId",
                table: "PacotesProduto",
                column: "PacoteId",
                principalTable: "Pacotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoImagens_Produtos_ProdutoId",
                table: "ProdutoImagens",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoRelacionados_Produtos_ProdutoId",
                table: "ProdutoRelacionados",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PacotesProduto_Pacotes_PacoteId",
                table: "PacotesProduto");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoImagens_Produtos_ProdutoId",
                table: "ProdutoImagens");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoRelacionados_Produtos_ProdutoId",
                table: "ProdutoRelacionados");

            migrationBuilder.AddForeignKey(
                name: "FK_PacotesProduto_Pacotes_PacoteId",
                table: "PacotesProduto",
                column: "PacoteId",
                principalTable: "Pacotes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoImagens_Produtos_ProdutoId",
                table: "ProdutoImagens",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutoRelacionados_Produtos_ProdutoId",
                table: "ProdutoRelacionados",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");
        }
    }
}
