using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PoetSite.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGalleryImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "GalleryImages",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$TeKwmMM3MsNJblc6O8S3PuUU2P9eC.ExbKz1pNaCTiepBYY8SDYEq");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "GalleryImages");

            migrationBuilder.UpdateData(
                table: "AdminUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$.ADKKlb/AWgZdzLAs3rx8.HJjNkTPTpaquBfG7NL.DbJ7tbiwFOye");
        }
    }
}
