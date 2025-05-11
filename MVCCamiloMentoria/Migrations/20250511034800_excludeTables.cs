using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCCamiloMentoria.Migrations
{
    /// <inheritdoc />
    public partial class excludeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Turma",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Supervisor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "Excluido",
                table: "Professor",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Escola",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Equipamento",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Diretor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Coordenador",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Aluno",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Turma");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Supervisor");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Escola");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Equipamento");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Diretor");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Coordenador");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Aluno");

            migrationBuilder.AlterColumn<bool>(
                name: "Excluido",
                table: "Professor",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }
    }
}
