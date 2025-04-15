using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class ajustenametables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NomeEmpresa",
                table: "Fornecedor",
                newName: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Fornecedor",
                newName: "NomeEmpresa");
        }
    }
}
