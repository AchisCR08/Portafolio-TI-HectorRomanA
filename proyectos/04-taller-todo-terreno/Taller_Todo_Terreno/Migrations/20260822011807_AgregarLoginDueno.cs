using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taller_Todo_Terreno.Migrations
{
    /// <inheritdoc />
    public partial class AgregarLoginDueno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contrasena",
                table: "Duenos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contrasena",
                table: "Duenos");
        }
    }
}
