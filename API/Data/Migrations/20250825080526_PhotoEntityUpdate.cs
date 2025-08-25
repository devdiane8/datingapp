using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class PhotoEntityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Members",
                newName: "LastActive");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Members",
                newName: "Created");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastActive",
                table: "Members",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Members",
                newName: "CreatedAt");
        }
    }
}
