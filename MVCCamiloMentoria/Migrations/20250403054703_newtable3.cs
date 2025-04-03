using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class newtable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_turmas",
                table: "turmas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_alunos",
                table: "alunos");

            migrationBuilder.RenameTable(
                name: "turmas",
                newName: "Turma");

            migrationBuilder.RenameTable(
                name: "alunos",
                newName: "Aluno");

            migrationBuilder.RenameColumn(
                name: "AlunosTurma",
                table: "Turma",
                newName: "EscolaId");

            migrationBuilder.RenameColumn(
                name: "AnoLetivo",
                table: "Aluno",
                newName: "EscolaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Aluno",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "NomeTurma",
                table: "Turma",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "AnoLetivo",
                table: "Turma",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Turno",
                table: "Turma",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "NomeAluno",
                table: "Aluno",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EmailEscolar",
                table: "Aluno",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<bool>(
                name: "BolsaEscolar",
                table: "Aluno",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Aluno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Aluno",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Turma",
                table: "Turma",
                column: "TurmaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aluno",
                table: "Aluno",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    EnderecoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeRua = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CEP = table.Column<int>(type: "int", maxLength: 8, nullable: false),
                    NumeroRua = table.Column<int>(type: "int", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.EnderecoId);
                });

            migrationBuilder.CreateTable(
                name: "MarcaEquipamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(190)", maxLength: 190, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(600)", maxLength: 600, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcaEquipamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diretores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Matricula = table.Column<int>(type: "int", nullable: true),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diretores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diretores_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Responsavel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsavel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responsavel_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Supervisor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Matricula = table.Column<int>(type: "int", maxLength: 6, nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supervisor_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modelos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarcaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modelos_MarcaEquipamento_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "MarcaEquipamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlunoResponsavel",
                columns: table => new
                {
                    AlunosAlunoId = table.Column<int>(type: "int", nullable: false),
                    ResponsaveisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoResponsavel", x => new { x.AlunosAlunoId, x.ResponsaveisId });
                    table.ForeignKey(
                        name: "FK_AlunoResponsavel_Aluno_AlunosAlunoId",
                        column: x => x.AlunosAlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoResponsavel_Responsavel_ResponsaveisId",
                        column: x => x.ResponsaveisId,
                        principalTable: "Responsavel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aula",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HorarioInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HorarioFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfessorId = table.Column<int>(type: "int", nullable: false),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    DisciplinaId = table.Column<int>(type: "int", nullable: false),
                    ConfirmacaoPresenca = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aula_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "TurmaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PresencaAula",
                columns: table => new
                {
                    AlunoId = table.Column<int>(type: "int", nullable: false),
                    AulaId = table.Column<int>(type: "int", nullable: false),
                    Presente = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresencaAula", x => new { x.AlunoId, x.AulaId });
                    table.ForeignKey(
                        name: "FK_PresencaAula_Aluno_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Aluno",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PresencaAula_Aula_AulaId",
                        column: x => x.AulaId,
                        principalTable: "Aula",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coordenador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Matricula = table.Column<int>(type: "int", maxLength: 6, nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    Telefone = table.Column<int>(type: "int", maxLength: 12, nullable: false),
                    EscolaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordenador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coordenador_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EscolaId = table.Column<int>(type: "int", nullable: false),
                    EscolaId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplina", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Escola",
                columns: table => new
                {
                    EscolaId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escola", x => x.EscolaId);
                    table.ForeignKey(
                        name: "FK_Escola_Disciplina_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Escola_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TurmaDisciplina",
                columns: table => new
                {
                    DisciplinasId = table.Column<int>(type: "int", nullable: false),
                    TurmasTurmaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaDisciplina", x => new { x.DisciplinasId, x.TurmasTurmaId });
                    table.ForeignKey(
                        name: "FK_TurmaDisciplina_Disciplina_DisciplinasId",
                        column: x => x.DisciplinasId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurmaDisciplina_Turma_TurmasTurmaId",
                        column: x => x.TurmasTurmaId,
                        principalTable: "Turma",
                        principalColumn: "TurmaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Equipamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusOperacional = table.Column<bool>(type: "bit", nullable: false),
                    MarcaId = table.Column<int>(type: "int", nullable: false),
                    ModeloId = table.Column<int>(type: "int", nullable: false),
                    EscolaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipamentos_Escola_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escola",
                        principalColumn: "EscolaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipamentos_MarcaEquipamento_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "MarcaEquipamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Equipamentos_Modelos_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "Modelos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EscolaSupervisor",
                columns: table => new
                {
                    EscolasEscolaId = table.Column<int>(type: "int", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscolaSupervisor", x => new { x.EscolasEscolaId, x.SupervisorId });
                    table.ForeignKey(
                        name: "FK_EscolaSupervisor_Escola_EscolasEscolaId",
                        column: x => x.EscolasEscolaId,
                        principalTable: "Escola",
                        principalColumn: "EscolaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EscolaSupervisor_Supervisor_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "Supervisor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fornecedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeEmpresa = table.Column<string>(type: "nvarchar(190)", maxLength: 190, nullable: false),
                    CNPJ = table.Column<int>(type: "int", maxLength: 14, nullable: true),
                    CPF = table.Column<int>(type: "int", maxLength: 11, nullable: true),
                    FinalidadeFornecedor = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EscolaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fornecedor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fornecedor_Escola_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escola",
                        principalColumn: "EscolaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrestadoresServico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<int>(type: "int", nullable: true),
                    CNPJ = table.Column<int>(type: "int", nullable: true),
                    EmpresaNome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServicoFinalidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EscolaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrestadoresServico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrestadoresServico_Escola_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escola",
                        principalColumn: "EscolaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Matricula = table.Column<int>(type: "int", maxLength: 6, nullable: false),
                    EnderecoId = table.Column<int>(type: "int", nullable: false),
                    EscolaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Professor_Endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "Endereco",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Professor_Escola_EscolaId",
                        column: x => x.EscolaId,
                        principalTable: "Escola",
                        principalColumn: "EscolaId");
                });

            migrationBuilder.CreateTable(
                name: "ProfessorDisciplina",
                columns: table => new
                {
                    DisciplinasId = table.Column<int>(type: "int", nullable: false),
                    ProfessoresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorDisciplina", x => new { x.DisciplinasId, x.ProfessoresId });
                    table.ForeignKey(
                        name: "FK_ProfessorDisciplina_Disciplina_DisciplinasId",
                        column: x => x.DisciplinasId,
                        principalTable: "Disciplina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorDisciplina_Professor_ProfessoresId",
                        column: x => x.ProfessoresId,
                        principalTable: "Professor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfessorTurma",
                columns: table => new
                {
                    ProfessorTurmaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TurmaId = table.Column<int>(type: "int", nullable: false),
                    ProfessorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessorTurma", x => x.ProfessorTurmaId);
                    table.ForeignKey(
                        name: "FK_ProfessorTurma_Professor_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessorTurma_Turma_TurmaId",
                        column: x => x.TurmaId,
                        principalTable: "Turma",
                        principalColumn: "TurmaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Turma_EscolaId",
                table: "Turma",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_EnderecoId",
                table: "Aluno",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_EscolaId",
                table: "Aluno",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_TurmaId",
                table: "Aluno",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoResponsavel_ResponsaveisId",
                table: "AlunoResponsavel",
                column: "ResponsaveisId");

            migrationBuilder.CreateIndex(
                name: "IX_Aula_DisciplinaId",
                table: "Aula",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Aula_ProfessorId",
                table: "Aula",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Aula_TurmaId",
                table: "Aula",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordenador_EnderecoId",
                table: "Coordenador",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coordenador_EscolaId",
                table: "Coordenador",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Diretores_EnderecoId",
                table: "Diretores",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplina_EscolaId1",
                table: "Disciplina",
                column: "EscolaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_EscolaId",
                table: "Equipamentos",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_MarcaId",
                table: "Equipamentos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipamentos_ModeloId",
                table: "Equipamentos",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_Escola_EnderecoId",
                table: "Escola",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EscolaSupervisor_SupervisorId",
                table: "EscolaSupervisor",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Fornecedor_EscolaId",
                table: "Fornecedor",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Modelos_MarcaId",
                table: "Modelos",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_PresencaAula_AulaId",
                table: "PresencaAula",
                column: "AulaId");

            migrationBuilder.CreateIndex(
                name: "IX_PrestadoresServico_EscolaId",
                table: "PrestadoresServico",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_Professor_EnderecoId",
                table: "Professor",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professor_EscolaId",
                table: "Professor",
                column: "EscolaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorDisciplina_ProfessoresId",
                table: "ProfessorDisciplina",
                column: "ProfessoresId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorTurma_ProfessorId",
                table: "ProfessorTurma",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessorTurma_TurmaId",
                table: "ProfessorTurma",
                column: "TurmaId");

            migrationBuilder.CreateIndex(
                name: "IX_Responsavel_EnderecoId",
                table: "Responsavel",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisor_EnderecoId",
                table: "Supervisor",
                column: "EnderecoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TurmaDisciplina_TurmasTurmaId",
                table: "TurmaDisciplina",
                column: "TurmasTurmaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Endereco_EnderecoId",
                table: "Aluno",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Escola_EscolaId",
                table: "Aluno",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "EscolaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Turma_TurmaId",
                table: "Aluno",
                column: "TurmaId",
                principalTable: "Turma",
                principalColumn: "TurmaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Turma_Escola_EscolaId",
                table: "Turma",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "EscolaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Aula_Disciplina_DisciplinaId",
                table: "Aula",
                column: "DisciplinaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aula_Disciplina_TurmaId",
                table: "Aula",
                column: "TurmaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Aula_Professor_ProfessorId",
                table: "Aula",
                column: "ProfessorId",
                principalTable: "Professor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Coordenador_Escola_EscolaId",
                table: "Coordenador",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "EscolaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplina_Escola_EscolaId1",
                table: "Disciplina",
                column: "EscolaId1",
                principalTable: "Escola",
                principalColumn: "EscolaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Endereco_EnderecoId",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Escola_EscolaId",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Turma_TurmaId",
                table: "Aluno");

            migrationBuilder.DropForeignKey(
                name: "FK_Turma_Escola_EscolaId",
                table: "Turma");

            migrationBuilder.DropForeignKey(
                name: "FK_Escola_Disciplina_EscolaId",
                table: "Escola");

            migrationBuilder.DropTable(
                name: "AlunoResponsavel");

            migrationBuilder.DropTable(
                name: "Coordenador");

            migrationBuilder.DropTable(
                name: "Diretores");

            migrationBuilder.DropTable(
                name: "Equipamentos");

            migrationBuilder.DropTable(
                name: "EscolaSupervisor");

            migrationBuilder.DropTable(
                name: "Fornecedor");

            migrationBuilder.DropTable(
                name: "PresencaAula");

            migrationBuilder.DropTable(
                name: "PrestadoresServico");

            migrationBuilder.DropTable(
                name: "ProfessorDisciplina");

            migrationBuilder.DropTable(
                name: "ProfessorTurma");

            migrationBuilder.DropTable(
                name: "TurmaDisciplina");

            migrationBuilder.DropTable(
                name: "Responsavel");

            migrationBuilder.DropTable(
                name: "Modelos");

            migrationBuilder.DropTable(
                name: "Supervisor");

            migrationBuilder.DropTable(
                name: "Aula");

            migrationBuilder.DropTable(
                name: "MarcaEquipamento");

            migrationBuilder.DropTable(
                name: "Professor");

            migrationBuilder.DropTable(
                name: "Disciplina");

            migrationBuilder.DropTable(
                name: "Escola");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Turma",
                table: "Turma");

            migrationBuilder.DropIndex(
                name: "IX_Turma_EscolaId",
                table: "Turma");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aluno",
                table: "Aluno");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_EnderecoId",
                table: "Aluno");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_EscolaId",
                table: "Aluno");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_TurmaId",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "AnoLetivo",
                table: "Turma");

            migrationBuilder.DropColumn(
                name: "Turno",
                table: "Turma");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Aluno");

            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Aluno");

            migrationBuilder.RenameTable(
                name: "Turma",
                newName: "turmas");

            migrationBuilder.RenameTable(
                name: "Aluno",
                newName: "alunos");

            migrationBuilder.RenameColumn(
                name: "EscolaId",
                table: "turmas",
                newName: "AlunosTurma");

            migrationBuilder.RenameColumn(
                name: "EscolaId",
                table: "alunos",
                newName: "AnoLetivo");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "alunos",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "NomeTurma",
                table: "turmas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "NomeAluno",
                table: "alunos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "EmailEscolar",
                table: "alunos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "BolsaEscolar",
                table: "alunos",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_turmas",
                table: "turmas",
                column: "TurmaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_alunos",
                table: "alunos",
                column: "Id");
        }
    }
}
