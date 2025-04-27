using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class tableonrelationship2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Endereco_EnderecoId",
                table: "Professor");

            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Escola_EscolaId",
                table: "Professor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorDisciplina_Disciplina_DisciplinasId",
                table: "ProfessorDisciplina");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorDisciplina_Professor_ProfessoresId",
                table: "ProfessorDisciplina");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorTurma_Professor_ProfessorId",
                table: "ProfessorTurma");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefone_Professor_ProfessorId",
                table: "Telefone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorDisciplina",
                table: "ProfessorDisciplina");

            migrationBuilder.RenameColumn(
                name: "ProfessoresId",
                table: "ProfessorDisciplina",
                newName: "ProfessorId");

            migrationBuilder.RenameColumn(
                name: "DisciplinasId",
                table: "ProfessorDisciplina",
                newName: "DisciplinaId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorDisciplina_ProfessoresId",
                table: "ProfessorDisciplina",
                newName: "IX_ProfessorDisciplina_ProfessorId");

            migrationBuilder.AddColumn<int>(
                name: "ProfessorDisciplinaId",
                table: "ProfessorDisciplina",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "EscolaId1",
                table: "Professor",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorDisciplina",
                table: "ProfessorDisciplina",
                column: "ProfessorDisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorDisciplina_DisciplinaId",
                table: "ProfessorDisciplina",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_EscolaId1",
                table: "Professor",
                column: "EscolaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Endereco_EnderecoId",
                table: "Professor",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Escola_EscolaId",
                table: "Professor",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Escola_EscolaId1",
                table: "Professor",
                column: "EscolaId1",
                principalTable: "Escola",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorDisciplina_Disciplina_DisciplinaId",
                table: "ProfessorDisciplina",
                column: "DisciplinaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorDisciplina_Professor_ProfessorId",
                table: "ProfessorDisciplina",
                column: "ProfessorId",
                principalTable: "Professor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorTurma_Professor_ProfessorId",
                table: "ProfessorTurma",
                column: "ProfessorId",
                principalTable: "Professor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Telefone_Professor_ProfessorId",
                table: "Telefone",
                column: "ProfessorId",
                principalTable: "Professor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Endereco_EnderecoId",
                table: "Professor");

            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Escola_EscolaId",
                table: "Professor");

            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Escola_EscolaId1",
                table: "Professor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorDisciplina_Disciplina_DisciplinaId",
                table: "ProfessorDisciplina");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorDisciplina_Professor_ProfessorId",
                table: "ProfessorDisciplina");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorTurma_Professor_ProfessorId",
                table: "ProfessorTurma");

            migrationBuilder.DropForeignKey(
                name: "FK_Telefone_Professor_ProfessorId",
                table: "Telefone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessorDisciplina",
                table: "ProfessorDisciplina");

            migrationBuilder.DropIndex(
                name: "IX_ProfessorDisciplina_DisciplinaId",
                table: "ProfessorDisciplina");

            migrationBuilder.DropIndex(
                name: "IX_Professor_EscolaId1",
                table: "Professor");

            migrationBuilder.DropColumn(
                name: "ProfessorDisciplinaId",
                table: "ProfessorDisciplina");

            migrationBuilder.DropColumn(
                name: "EscolaId1",
                table: "Professor");

            migrationBuilder.RenameColumn(
                name: "ProfessorId",
                table: "ProfessorDisciplina",
                newName: "ProfessoresId");

            migrationBuilder.RenameColumn(
                name: "DisciplinaId",
                table: "ProfessorDisciplina",
                newName: "DisciplinasId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorDisciplina_ProfessorId",
                table: "ProfessorDisciplina",
                newName: "IX_ProfessorDisciplina_ProfessoresId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessorDisciplina",
                table: "ProfessorDisciplina",
                columns: new[] { "DisciplinasId", "ProfessoresId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Endereco_EnderecoId",
                table: "Professor",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Escola_EscolaId",
                table: "Professor",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorDisciplina_Disciplina_DisciplinasId",
                table: "ProfessorDisciplina",
                column: "DisciplinasId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorDisciplina_Professor_ProfessoresId",
                table: "ProfessorDisciplina",
                column: "ProfessoresId",
                principalTable: "Professor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorTurma_Professor_ProfessorId",
                table: "ProfessorTurma",
                column: "ProfessorId",
                principalTable: "Professor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Telefone_Professor_ProfessorId",
                table: "Telefone",
                column: "ProfessorId",
                principalTable: "Professor",
                principalColumn: "Id");
        }
    }
}
