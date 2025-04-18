using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class updatetables1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aula_Disciplina_TurmaId",
                table: "Aula");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaDisciplina_Disciplina_DisciplinasId",
                table: "TurmaDisciplina");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaDisciplina_Turma_TurmaId",
                table: "TurmaDisciplina");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TurmaDisciplina",
                table: "TurmaDisciplina");

            migrationBuilder.DropIndex(
                name: "IX_TurmaDisciplina_TurmaId",
                table: "TurmaDisciplina");

            migrationBuilder.RenameColumn(
                name: "DisciplinasId",
                table: "TurmaDisciplina",
                newName: "DisciplinaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TurmaDisciplina",
                table: "TurmaDisciplina",
                columns: new[] { "TurmaId", "DisciplinaId" });

            migrationBuilder.CreateIndex(
                name: "IX_TurmaDisciplina_DisciplinaId",
                table: "TurmaDisciplina",
                column: "DisciplinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaDisciplina_Disciplina_DisciplinaId",
                table: "TurmaDisciplina",
                column: "DisciplinaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaDisciplina_Turma_TurmaId",
                table: "TurmaDisciplina",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "TurmaId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TurmaDisciplina_Disciplina_DisciplinaId",
                table: "TurmaDisciplina");

            migrationBuilder.DropForeignKey(
                name: "FK_TurmaDisciplina_Turma_TurmaId",
                table: "TurmaDisciplina");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TurmaDisciplina",
                table: "TurmaDisciplina");

            migrationBuilder.DropIndex(
                name: "IX_TurmaDisciplina_DisciplinaId",
                table: "TurmaDisciplina");

            migrationBuilder.RenameColumn(
                name: "DisciplinaId",
                table: "TurmaDisciplina",
                newName: "DisciplinasId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TurmaDisciplina",
                table: "TurmaDisciplina",
                columns: new[] { "DisciplinasId", "TurmaId" });

            migrationBuilder.CreateIndex(
                name: "IX_TurmaDisciplina_TurmaId",
                table: "TurmaDisciplina",
                column: "TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aula_Disciplina_TurmaId",
                table: "Aula",
                column: "TurmaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaDisciplina_Disciplina_DisciplinasId",
                table: "TurmaDisciplina",
                column: "DisciplinasId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TurmaDisciplina_Turma_TurmaId",
                table: "TurmaDisciplina",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "TurmaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
