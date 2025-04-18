using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class updatetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EscolaId",
                table: "Aula",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Aula_EscolaId",
                table: "Aula",
                column: "EscolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aula_Escola_EscolaId",
                table: "Aula",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aula_Escola_EscolaId",
                table: "Aula");

            migrationBuilder.DropIndex(
                name: "IX_Aula_EscolaId",
                table: "Aula");

            migrationBuilder.DropColumn(
                name: "EscolaId",
                table: "Aula");
        }
    }
}
