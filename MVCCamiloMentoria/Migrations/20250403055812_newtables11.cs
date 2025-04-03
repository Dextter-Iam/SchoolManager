using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class newtables11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escola_Disciplina_Id",
                table: "Escola");

            migrationBuilder.DropForeignKey(
                name: "FK_EscolaSupervisor_Escola_EscolasId",
                table: "EscolaSupervisor");

            migrationBuilder.RenameColumn(
                name: "EscolasId",
                table: "EscolaSupervisor",
                newName: "EscolasEscolaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Escola",
                newName: "EscolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Escola_Disciplina_EscolaId",
                table: "Escola",
                column: "EscolaId",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EscolaSupervisor_Escola_EscolasEscolaId",
                table: "EscolaSupervisor",
                column: "EscolasEscolaId",
                principalTable: "Escola",
                principalColumn: "EscolaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escola_Disciplina_EscolaId",
                table: "Escola");

            migrationBuilder.DropForeignKey(
                name: "FK_EscolaSupervisor_Escola_EscolasEscolaId",
                table: "EscolaSupervisor");

            migrationBuilder.RenameColumn(
                name: "EscolasEscolaId",
                table: "EscolaSupervisor",
                newName: "EscolasId");

            migrationBuilder.RenameColumn(
                name: "EscolaId",
                table: "Escola",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Escola_Disciplina_Id",
                table: "Escola",
                column: "Id",
                principalTable: "Disciplina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EscolaSupervisor_Escola_EscolasId",
                table: "EscolaSupervisor",
                column: "EscolasId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
