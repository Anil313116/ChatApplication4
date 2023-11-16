﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApplication4.Migrations
{
    /// <inheritdoc />
    public partial class CustomUsersUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "CustomUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "CustomUsers");
        }
    }
}
