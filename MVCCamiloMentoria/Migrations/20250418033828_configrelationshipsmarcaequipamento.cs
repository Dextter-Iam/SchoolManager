using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class configrelationshipsmarcaequipamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipamento_Modelo_ModeloId",
                table: "Equipamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Modelo_MarcaEquipamento_MarcaId",
                table: "Modelo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modelo",
                table: "Modelo");

            migrationBuilder.RenameTable(
                name: "Modelo",
                newName: "ModeloEquipamento");

            migrationBuilder.RenameIndex(
                name: "IX_Modelo_MarcaId",
                table: "ModeloEquipamento",
                newName: "IX_ModeloEquipamento_MarcaId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "ModeloEquipamento",
                type: "nvarchar(190)",
                maxLength: 190,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "ModeloEquipamento",
                type: "nvarchar(600)",
                maxLength: 600,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ModeloEquipamento",
                table: "ModeloEquipamento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamento_ModeloEquipamento_ModeloId",
                table: "Equipamento",
                column: "ModeloId",
                principalTable: "ModeloEquipamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ModeloEquipamento_MarcaEquipamento_MarcaId",
                table: "ModeloEquipamento",
                column: "MarcaId",
                principalTable: "MarcaEquipamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipamento_ModeloEquipamento_ModeloId",
                table: "Equipamento");

            migrationBuilder.DropForeignKey(
                name: "FK_ModeloEquipamento_MarcaEquipamento_MarcaId",
                table: "ModeloEquipamento");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ModeloEquipamento",
                table: "ModeloEquipamento");

            migrationBuilder.RenameTable(
                name: "ModeloEquipamento",
                newName: "Modelo");

            migrationBuilder.RenameIndex(
                name: "IX_ModeloEquipamento_MarcaId",
                table: "Modelo",
                newName: "IX_Modelo_MarcaId");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Modelo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(190)",
                oldMaxLength: 190);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Modelo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(600)",
                oldMaxLength: 600);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modelo",
                table: "Modelo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipamento_Modelo_ModeloId",
                table: "Equipamento",
                column: "ModeloId",
                principalTable: "Modelo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Modelo_MarcaEquipamento_MarcaId",
                table: "Modelo",
                column: "MarcaId",
                principalTable: "MarcaEquipamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
