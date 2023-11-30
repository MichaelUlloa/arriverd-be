using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace arriverd_be.Migrations
{
    /// <inheritdoc />
    public partial class Image_ImageId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Images");
        }
    }
}
