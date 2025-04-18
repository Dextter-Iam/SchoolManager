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
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Telefone_TelefoneId",
                table: "Aluno");

            migrationBuilder.RenameColumn(
                name: "TelefoneId",
                table: "Aluno",
                newName: "EstadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Aluno_TelefoneId",
                table: "Aluno",
                newName: "IX_Aluno_EstadoId");

            migrationBuilder.AddColumn<int>(
                name: "DDD",
                table: "AlunoTelefone",
                type: "int",
                maxLength: 3,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                table: "AlunoTelefone",
                type: "int",
                maxLength: 12,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Estado_EstadoId",
                table: "Aluno",
                column: "EstadoId",
                principalTable: "Estado",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Estado_EstadoId",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "DDD",
                table: "AlunoTelefone");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "AlunoTelefone");

            migrationBuilder.RenameColumn(
                name: "EstadoId",
                table: "Aluno",
                newName: "TelefoneId");

            migrationBuilder.RenameIndex(
                name: "IX_Aluno_EstadoId",
                table: "Aluno",
                newName: "IX_Aluno_TelefoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Telefone_TelefoneId",
                table: "Aluno",
                column: "TelefoneId",
                principalTable: "Telefone",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
