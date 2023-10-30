using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApplication4.Migrations
{
    /// <inheritdoc />
    public partial class CustomUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "CustomUsers",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "CustomUsers");
        }
    }
}
