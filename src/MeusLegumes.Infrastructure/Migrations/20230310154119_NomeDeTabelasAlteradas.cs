using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeusLegumes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NomeDeTabelasAlteradas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosImagem_Produtos_ProdutoId",
                table: "ProdutosImagem");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutosRelacionado_Produtos_ProdutoId",
                table: "ProdutosRelacionado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutosRelacionado",
                table: "ProdutosRelacionado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutosImagem",
                table: "ProdutosImagem");

            migrationBuilder.RenameTable(
                name: "ProdutosRelacionado",
                newName: "ProdutoRelacionados");

            migrationBuilder.RenameTable(
                name: "ProdutosImagem",
                newName: "ProdutoImagens");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutosRelacionado_ProdutoId",
                table: "ProdutoRelacionados",
                newName: "IX_ProdutoRelacionados_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutosImagem_ProdutoId",
                table: "ProdutoImagens",
                newName: "IX_ProdutoImagens_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutoRelacionados",
                table: "ProdutoRelacionados",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutoImagens",
                table: "ProdutoImagens",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoImagens_Produtos_ProdutoId",
                table: "ProdutoImagens");

            migrationBuilder.DropForeignKey(
                name: "FK_ProdutoRelacionados_Produtos_ProdutoId",
                table: "ProdutoRelacionados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutoRelacionados",
                table: "ProdutoRelacionados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProdutoImagens",
                table: "ProdutoImagens");

            migrationBuilder.RenameTable(
                name: "ProdutoRelacionados",
                newName: "ProdutosRelacionado");

            migrationBuilder.RenameTable(
                name: "ProdutoImagens",
                newName: "ProdutosImagem");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoRelacionados_ProdutoId",
                table: "ProdutosRelacionado",
                newName: "IX_ProdutosRelacionado_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProdutoImagens_ProdutoId",
                table: "ProdutosImagem",
                newName: "IX_ProdutosImagem_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutosRelacionado",
                table: "ProdutosRelacionado",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProdutosImagem",
                table: "ProdutosImagem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosImagem_Produtos_ProdutoId",
                table: "ProdutosImagem",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProdutosRelacionado_Produtos_ProdutoId",
                table: "ProdutosRelacionado",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");
        }
    }
}
