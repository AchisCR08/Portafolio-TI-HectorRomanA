using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taller_Todo_Terreno.Migrations
{
    /// <inheritdoc />
    public partial class CupoMaximoHorarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Citas_HorarioId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "Disponible",
                table: "Horarios");

            migrationBuilder.AddColumn<int>(
                name: "CupoMaximo",
                table: "Horarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Citas_HorarioId",
                table: "Citas",
                column: "HorarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Citas_HorarioId",
                table: "Citas");

            migrationBuilder.DropColumn(
                name: "CupoMaximo",
                table: "Horarios");

            migrationBuilder.AddColumn<bool>(
                name: "Disponible",
                table: "Horarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Citas_HorarioId",
                table: "Citas",
                column: "HorarioId",
                unique: true);
        }
    }
}
