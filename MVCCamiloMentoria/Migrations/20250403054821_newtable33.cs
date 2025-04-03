using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class newtable33 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlunoResponsavel_Aluno_AlunosAlunoId",
                table: "AlunoResponsavel");

            migrationBuilder.DropForeignKey(
                name: "FK_PresencaAula_Aula_AulaId",
                table: "PresencaAula");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PresencaAula",
                table: "PresencaAula");

            migrationBuilder.RenameColumn(
                name: "AlunosAlunoId",
                table: "AlunoResponsavel",
                newName: "AlunosId");

            migrationBuilder.AlterColumn<int>(
                name: "AulaId",
                table: "PresencaAula",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PresencaAula",
                table: "PresencaAula",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoResponsavel_Aluno_AlunosId",
                table: "AlunoResponsavel",
                column: "AlunosId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PresencaAula_Aula_AulaId",
                table: "PresencaAula",
                column: "AulaId",
                principalTable: "Aula",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AlunoResponsavel_Aluno_AlunosId",
                table: "AlunoResponsavel");

            migrationBuilder.DropForeignKey(
                name: "FK_PresencaAula_Aula_AulaId",
                table: "PresencaAula");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PresencaAula",
                table: "PresencaAula");

            migrationBuilder.RenameColumn(
                name: "AlunosId",
                table: "AlunoResponsavel",
                newName: "AlunosAlunoId");

            migrationBuilder.AlterColumn<int>(
                name: "AulaId",
                table: "PresencaAula",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PresencaAula",
                table: "PresencaAula",
                columns: new[] { "Id", "AulaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AlunoResponsavel_Aluno_AlunosAlunoId",
                table: "AlunoResponsavel",
                column: "AlunosAlunoId",
                principalTable: "Aluno",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PresencaAula_Aula_AulaId",
                table: "PresencaAula",
                column: "AulaId",
                principalTable: "Aula",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
