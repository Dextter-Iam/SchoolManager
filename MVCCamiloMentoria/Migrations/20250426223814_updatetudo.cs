using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class updatetudo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NomeResponsavel1",
                table: "Aluno",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeResponsavel2",
                table: "Aluno",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Parentesco1",
                table: "Aluno",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Parentesco2",
                table: "Aluno",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeResponsavel1",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "NomeResponsavel2",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "Parentesco1",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "Parentesco2",
                table: "Aluno");
        }
    }
}
