using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class firstbuildertables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alunos",
                columns: table => new
                {
                    AlunoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeAluno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmailEscolar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnoLetivo = table.Column<int>(type: "int", nullable: false),
                    AnoInscricao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BolsaEscolar = table.Column<bool>(type: "bit", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alunos", x => x.AlunoId);
                });

            migrationBuilder.CreateTable(
                name: "turmas",
                columns: table => new
                {
                    TurmaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlunosTurma = table.Column<int>(type: "int", nullable: false),
                    NomeTurma = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_turmas", x => x.TurmaId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alunos");

            migrationBuilder.DropTable(
                name: "turmas");
        }
    }
}
