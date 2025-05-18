using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professor_Escola_EscolaId1",
                table: "Professor");

            migrationBuilder.DropIndex(
                name: "IX_Professor_EscolaId1",
                table: "Professor");

            migrationBuilder.DropColumn(
                name: "EscolaId1",
                table: "Professor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EscolaId1",
                table: "Professor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professor_EscolaId1",
                table: "Professor",
                column: "EscolaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Professor_Escola_EscolaId1",
                table: "Professor",
                column: "EscolaId1",
                principalTable: "Escola",
                principalColumn: "Id");
        }
    }
}
