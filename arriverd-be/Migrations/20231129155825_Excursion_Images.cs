using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace arriverd_be.Migrations
{
    /// <inheritdoc />
    public partial class Excursion_Images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Images",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Images",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");
        }
    }
}
