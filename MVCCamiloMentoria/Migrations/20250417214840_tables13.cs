using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class tables13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escola_Estado_EstadoId",
                table: "Escola");

            migrationBuilder.AlterColumn<int>(
                name: "EstadoId",
                table: "Escola",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Escola_Estado_EstadoId",
                table: "Escola",
                column: "EstadoId",
                principalTable: "Estado",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Escola_Estado_EstadoId",
                table: "Escola");

            migrationBuilder.AlterColumn<int>(
                name: "EstadoId",
                table: "Escola",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Escola_Estado_EstadoId",
                table: "Escola",
                column: "EstadoId",
                principalTable: "Estado",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
