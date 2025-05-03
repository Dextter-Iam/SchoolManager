using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class tablesofspace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluno_Estado_EstadoId",
                table: "Aluno");

            migrationBuilder.DropIndex(
                name: "IX_Aluno_EstadoId",
                table: "Aluno");

            migrationBuilder.AddColumn<int>(
                name: "AlunoId",
                table: "Estado",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estado_AlunoId",
                table: "Estado",
                column: "AlunoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estado_Aluno_AlunoId",
                table: "Estado",
                column: "AlunoId",
                principalTable: "Aluno",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estado_Aluno_AlunoId",
                table: "Estado");

            migrationBuilder.DropIndex(
                name: "IX_Estado_AlunoId",
                table: "Estado");

            migrationBuilder.DropColumn(
                name: "AlunoId",
                table: "Estado");

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_EstadoId",
                table: "Aluno",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluno_Estado_EstadoId",
                table: "Aluno",
                column: "EstadoId",
                principalTable: "Estado",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
