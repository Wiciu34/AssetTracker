using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddDatesToAsset2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AssignmentDate",
                table: "FixedAssets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "expirationDate",
                table: "FixedAssets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "returnDate",
                table: "FixedAssets",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignmentDate",
                table: "FixedAssets");

            migrationBuilder.DropColumn(
                name: "expirationDate",
                table: "FixedAssets");

            migrationBuilder.DropColumn(
                name: "returnDate",
                table: "FixedAssets");
        }
    }
}
