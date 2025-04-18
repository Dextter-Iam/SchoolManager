using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class configrelationshipsmarcaequipamento1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipamento_ModeloEquipamento_ModeloId",
                table: "Equipamento");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamento_ModeloEquipamento_ModeloId",
                table: "Equipamento",
                column: "ModeloId",
                principalTable: "ModeloEquipamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipamento_ModeloEquipamento_ModeloId",
                table: "Equipamento");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamento_ModeloEquipamento_ModeloId",
                table: "Equipamento",
                column: "ModeloId",
                principalTable: "ModeloEquipamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
