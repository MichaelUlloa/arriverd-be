using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace arriverd_be.Migrations
{
    /// <inheritdoc />
    public partial class Capacity_Excursions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxReservations",
                table: "Excursions");

            migrationBuilder.RenameColumn(
                name: "MinReservations",
                table: "Excursions",
                newName: "Capacity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Excursions",
                newName: "MinReservations");

            migrationBuilder.AddColumn<short>(
                name: "MaxReservations",
                table: "Excursions",
                type: "smallint",
                nullable: true);
        }
    }
}
