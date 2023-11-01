using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Company.WPF.Migrations
{
    /// <inheritdoc />
    public partial class AddPenaltyDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CompensationDateTime",
                table: "Penalty",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CompensationDescription",
                table: "Penalty",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompensationDateTime",
                table: "Penalty");

            migrationBuilder.DropColumn(
                name: "CompensationDescription",
                table: "Penalty");
        }
    }
}
