using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class tableforpchouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Endereco_Id",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Turma_Id",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Aula_Disciplina_Id",
                table: "Aula");

            migrationBuilder.DropForeignKey(
                name: "FK_Aula_Turma_Id",
                table: "Aula");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordenador_Endereco_Id",
                table: "Coordenador");

            migrationBuilder.DropForeignKey(
                name: "FK_Diretores_Endereco_Id",
                table: "Diretores");

            migrationBuilder.DropForeignKey(
                name: "FK_Escola_Endereco_Id",
                table: "Escola");

            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Endereco_Id",
                table: "Professor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorTurma_Turma_Id",
                table: "ProfessorTurma");

            migrationBuilder.DropForeignKey(
                name: "FK_Responsavel_Endereco_Id",
                table: "Responsavel");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisor_Endereco_Id",
                table: "Supervisor");

            migrationBuilder.DropIndex(
                name: "IX_Supervisor_Id",
                table: "Supervisor");

            migrationBuilder.DropIndex(
                name: "IX_Responsavel_Id",
                table: "Responsavel");

            migrationBuilder.DropIndex(
                name: "IX_Professor_Id",
                table: "Professor");

            migrationBuilder.DropIndex(
                name: "IX_Escola_Id",
                table: "Escola");

            migrationBuilder.DropIndex(
                name: "IX_Diretores_Id",
                table: "Diretores");

            migrationBuilder.DropIndex(
                name: "IX_Coordenador_Id",
                table: "Coordenador");

            migrationBuilder.DropIndex(
                name: "IX_Aula_Id",
                table: "Aula");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_Id",
                table: "Aluno");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Turma",
                newName: "TurmaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProfessorTurma",
                newName: "TurmaId");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorTurma_Id",
                table: "ProfessorTurma",
                newName: "IX_ProfessorTurma_TurmaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Endereco",
                newName: "EnderecoId");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Supervisor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Responsavel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Professor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Escola",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Diretores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Coordenador",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TurmaId",
                table: "Aula",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Aluno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TurmaId",
                table: "Aluno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Supervisor_EnderecoId",
                table: "Supervisor",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responsavel_EnderecoId",
                table: "Responsavel",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_EnderecoId",
                table: "Professor",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Escola_EnderecoId",
                table: "Escola",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diretores_EnderecoId",
                table: "Diretores",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordenador_EnderecoId",
                table: "Coordenador",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aula_TurmaId",
                table: "Aula",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_EnderecoId",
                table: "Aluno",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_TurmaId",
                table: "Aluno",
                column: "TurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Endereco_EnderecoId",
                table: "Aluno",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Turma_TurmaId",
                table: "Aluno",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "TurmaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aula_Disciplina_TurmaId",
                table: "Aula",
                column: "TurmaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aula_Turma_TurmaId",
                table: "Aula",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "TurmaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordenador_Endereco_EnderecoId",
                table: "Coordenador",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diretores_Endereco_EnderecoId",
                table: "Diretores",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Escola_Endereco_EnderecoId",
                table: "Escola",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Endereco_EnderecoId",
                table: "Professor",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorTurma_Turma_TurmaId",
                table: "ProfessorTurma",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "TurmaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Responsavel_Endereco_EnderecoId",
                table: "Responsavel",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisor_Endereco_EnderecoId",
                table: "Supervisor",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Endereco_EnderecoId",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Turma_TurmaId",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Aula_Disciplina_TurmaId",
                table: "Aula");

            migrationBuilder.DropForeignKey(
                name: "FK_Aula_Turma_TurmaId",
                table: "Aula");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordenador_Endereco_EnderecoId",
                table: "Coordenador");

            migrationBuilder.DropForeignKey(
                name: "FK_Diretores_Endereco_EnderecoId",
                table: "Diretores");

            migrationBuilder.DropForeignKey(
                name: "FK_Escola_Endereco_EnderecoId",
                table: "Escola");

            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Endereco_EnderecoId",
                table: "Professor");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfessorTurma_Turma_TurmaId",
                table: "ProfessorTurma");

            migrationBuilder.DropForeignKey(
                name: "FK_Responsavel_Endereco_EnderecoId",
                table: "Responsavel");

            migrationBuilder.DropForeignKey(
                name: "FK_Supervisor_Endereco_EnderecoId",
                table: "Supervisor");

            migrationBuilder.DropIndex(
                name: "IX_Supervisor_EnderecoId",
                table: "Supervisor");

            migrationBuilder.DropIndex(
                name: "IX_Responsavel_EnderecoId",
                table: "Responsavel");

            migrationBuilder.DropIndex(
                name: "IX_Professor_EnderecoId",
                table: "Professor");

            migrationBuilder.DropIndex(
                name: "IX_Escola_EnderecoId",
                table: "Escola");

            migrationBuilder.DropIndex(
                name: "IX_Diretores_EnderecoId",
                table: "Diretores");

            migrationBuilder.DropIndex(
                name: "IX_Coordenador_EnderecoId",
                table: "Coordenador");

            migrationBuilder.DropIndex(
                name: "IX_Aula_TurmaId",
                table: "Aula");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_EnderecoId",
                table: "Aluno");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_TurmaId",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Supervisor");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Responsavel");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Professor");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Escola");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Diretores");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Coordenador");

            migrationBuilder.DropColumn(
                name: "TurmaId",
                table: "Aula");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "TurmaId",
                table: "Aluno");

            migrationBuilder.RenameColumn(
                name: "TurmaId",
                table: "Turma",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TurmaId",
                table: "ProfessorTurma",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ProfessorTurma_TurmaId",
                table: "ProfessorTurma",
                newName: "IX_ProfessorTurma_Id");

            migrationBuilder.RenameColumn(
                name: "EnderecoId",
                table: "Endereco",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisor_Id",
                table: "Supervisor",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responsavel_Id",
                table: "Responsavel",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_Id",
                table: "Professor",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Escola_Id",
                table: "Escola",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diretores_Id",
                table: "Diretores",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Coordenador_Id",
                table: "Coordenador",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aula_Id",
                table: "Aula",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_Id",
                table: "Aluno",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Endereco_Id",
                table: "Aluno",
                column: "Id",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Turma_Id",
                table: "Aluno",
                column: "Id",
                principalTable: "Turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aula_Disciplina_Id",
                table: "Aula",
                column: "Id",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aula_Turma_Id",
                table: "Aula",
                column: "Id",
                principalTable: "Turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordenador_Endereco_Id",
                table: "Coordenador",
                column: "Id",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Diretores_Endereco_Id",
                table: "Diretores",
                column: "Id",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Escola_Endereco_Id",
                table: "Escola",
                column: "Id",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Endereco_Id",
                table: "Professor",
                column: "Id",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfessorTurma_Turma_Id",
                table: "ProfessorTurma",
                column: "Id",
                principalTable: "Turma",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Responsavel_Endereco_Id",
                table: "Responsavel",
                column: "Id",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisor_Endereco_Id",
                table: "Supervisor",
                column: "Id",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
