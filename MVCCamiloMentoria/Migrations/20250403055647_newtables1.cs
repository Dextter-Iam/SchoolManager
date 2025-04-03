using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class newtables1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Escola_Id",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordenador_Escola_Id",
                table: "Coordenador");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipamentos_Escola_Id",
                table: "Equipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_EscolaSupervisor_Escola_EscolasEscolaId",
                table: "EscolaSupervisor");

            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedor_Escola_Id",
                table: "Fornecedor");

            migrationBuilder.DropForeignKey(
                name: "FK_PrestadoresServico_Escola_Id",
                table: "PrestadoresServico");

            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Escola_Id",
                table: "Professor");

            migrationBuilder.DropForeignKey(
                name: "FK_Turma_Escola_Id",
                table: "Turma");

            migrationBuilder.DropIndex(
                name: "IX_Professor_Id",
                table: "Professor");

            migrationBuilder.DropIndex(
                name: "IX_PrestadoresServico_Id",
                table: "PrestadoresServico");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedor_Id",
                table: "Fornecedor");

            migrationBuilder.DropIndex(
                name: "IX_Equipamentos_Id",
                table: "Equipamentos");

            migrationBuilder.DropIndex(
                name: "IX_Coordenador_Id",
                table: "Coordenador");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_Id",
                table: "Aluno");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Turma",
                newName: "EscolaId");

            migrationBuilder.RenameIndex(
                name: "IX_Turma_Id",
                table: "Turma",
                newName: "IX_Turma_EscolaId");

            migrationBuilder.RenameColumn(
                name: "EscolasEscolaId",
                table: "EscolaSupervisor",
                newName: "EscolasId");

            migrationBuilder.AddColumn<int>(
                name: "EscolaId",
                table: "Professor",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EscolaId",
                table: "PrestadoresServico",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EscolaId",
                table: "Fornecedor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EscolaId",
                table: "Equipamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EscolaId",
                table: "Disciplina",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EscolaId",
                table: "Coordenador",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EscolaId",
                table: "Aluno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Professor_EscolaId",
                table: "Professor",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestadoresServico_EscolaId",
                table: "PrestadoresServico",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_EscolaId",
                table: "Fornecedor",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_EscolaId",
                table: "Equipamentos",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordenador_EscolaId",
                table: "Coordenador",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_EscolaId",
                table: "Aluno",
                column: "EscolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Escola_EscolaId",
                table: "Aluno",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordenador_Escola_EscolaId",
                table: "Coordenador",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamentos_Escola_EscolaId",
                table: "Equipamentos",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EscolaSupervisor_Escola_EscolasId",
                table: "EscolaSupervisor",
                column: "EscolasId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedor_Escola_EscolaId",
                table: "Fornecedor",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrestadoresServico_Escola_EscolaId",
                table: "PrestadoresServico",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Escola_EscolaId",
                table: "Professor",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turma_Escola_EscolaId",
                table: "Turma",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Escola_EscolaId",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Coordenador_Escola_EscolaId",
                table: "Coordenador");

            migrationBuilder.DropForeignKey(
                name: "FK_Equipamentos_Escola_EscolaId",
                table: "Equipamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_EscolaSupervisor_Escola_EscolasId",
                table: "EscolaSupervisor");

            migrationBuilder.DropForeignKey(
                name: "FK_Fornecedor_Escola_EscolaId",
                table: "Fornecedor");

            migrationBuilder.DropForeignKey(
                name: "FK_PrestadoresServico_Escola_EscolaId",
                table: "PrestadoresServico");

            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Escola_EscolaId",
                table: "Professor");

            migrationBuilder.DropForeignKey(
                name: "FK_Turma_Escola_EscolaId",
                table: "Turma");

            migrationBuilder.DropIndex(
                name: "IX_Professor_EscolaId",
                table: "Professor");

            migrationBuilder.DropIndex(
                name: "IX_PrestadoresServico_EscolaId",
                table: "PrestadoresServico");

            migrationBuilder.DropIndex(
                name: "IX_Fornecedor_EscolaId",
                table: "Fornecedor");

            migrationBuilder.DropIndex(
                name: "IX_Equipamentos_EscolaId",
                table: "Equipamentos");

            migrationBuilder.DropIndex(
                name: "IX_Coordenador_EscolaId",
                table: "Coordenador");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_EscolaId",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "EscolaId",
                table: "Professor");

            migrationBuilder.DropColumn(
                name: "EscolaId",
                table: "PrestadoresServico");

            migrationBuilder.DropColumn(
                name: "EscolaId",
                table: "Fornecedor");

            migrationBuilder.DropColumn(
                name: "EscolaId",
                table: "Equipamentos");

            migrationBuilder.DropColumn(
                name: "EscolaId",
                table: "Disciplina");

            migrationBuilder.DropColumn(
                name: "EscolaId",
                table: "Coordenador");

            migrationBuilder.DropColumn(
                name: "EscolaId",
                table: "Aluno");

            migrationBuilder.RenameColumn(
                name: "EscolaId",
                table: "Turma",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Turma_EscolaId",
                table: "Turma",
                newName: "IX_Turma_Id");

            migrationBuilder.RenameColumn(
                name: "EscolasId",
                table: "EscolaSupervisor",
                newName: "EscolasEscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_Id",
                table: "Professor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PrestadoresServico_Id",
                table: "PrestadoresServico",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_Id",
                table: "Fornecedor",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_Id",
                table: "Equipamentos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Coordenador_Id",
                table: "Coordenador",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_Id",
                table: "Aluno",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Escola_Id",
                table: "Aluno",
                column: "Id",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordenador_Escola_Id",
                table: "Coordenador",
                column: "Id",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamentos_Escola_Id",
                table: "Equipamentos",
                column: "Id",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EscolaSupervisor_Escola_EscolasEscolaId",
                table: "EscolaSupervisor",
                column: "EscolasEscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Fornecedor_Escola_Id",
                table: "Fornecedor",
                column: "Id",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrestadoresServico_Escola_Id",
                table: "PrestadoresServico",
                column: "Id",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Escola_Id",
                table: "Professor",
                column: "Id",
                principalTable: "Escola",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Turma_Escola_Id",
                table: "Turma",
                column: "Id",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
