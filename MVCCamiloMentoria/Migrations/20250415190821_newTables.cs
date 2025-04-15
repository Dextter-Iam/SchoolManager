using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class newTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Escola",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Escola_EstadoId",
                table: "Escola",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Escola_Estado_EstadoId",
                table: "Escola",
                column: "EstadoId",
                principalTable: "Estado",
                principalColumn: "id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escola_Estado_EstadoId",
                table: "Escola");

            migrationBuilder.DropIndex(
                name: "IX_Escola_EstadoId",
                table: "Escola");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Escola");
        }
    }
}
