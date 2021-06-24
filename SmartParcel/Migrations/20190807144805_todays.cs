using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartParcel.Migrations
{
    public partial class todays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverLicence",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDriver",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverLicence",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDriver",
                table: "AspNetUsers");
        }
    }
}
