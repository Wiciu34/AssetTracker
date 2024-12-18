using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkplaceToAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Workplace",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Workplace",
                table: "AspNetUsers");
        }
    }
}
