using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class firstmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplina_Escola_EscolaId",
                table: "Disciplina");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaDisciplina_Turma_TurmasTurmaId",
                table: "TurmaDisciplina");

            migrationBuilder.RenameColumn(
                name: "TurmasTurmaId",
                table: "TurmaDisciplina",
                newName: "TurmaId");

            migrationBuilder.RenameIndex(
                name: "IX_TurmaDisciplina_TurmasTurmaId",
                table: "TurmaDisciplina",
                newName: "IX_TurmaDisciplina_TurmaId");

            migrationBuilder.AlterColumn<int>(
                name: "EscolaId",
                table: "Disciplina",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplina_Escola_EscolaId",
                table: "Disciplina",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaDisciplina_Turma_TurmaId",
                table: "TurmaDisciplina",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "TurmaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Disciplina_Escola_EscolaId",
                table: "Disciplina");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaDisciplina_Turma_TurmaId",
                table: "TurmaDisciplina");

            migrationBuilder.RenameColumn(
                name: "TurmaId",
                table: "TurmaDisciplina",
                newName: "TurmasTurmaId");

            migrationBuilder.RenameIndex(
                name: "IX_TurmaDisciplina_TurmaId",
                table: "TurmaDisciplina",
                newName: "IX_TurmaDisciplina_TurmasTurmaId");

            migrationBuilder.AlterColumn<int>(
                name: "EscolaId",
                table: "Disciplina",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplina_Escola_EscolaId",
                table: "Disciplina",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaDisciplina_Turma_TurmasTurmaId",
                table: "TurmaDisciplina",
                column: "TurmasTurmaId",
                principalTable: "Turma",
                principalColumn: "TurmaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
