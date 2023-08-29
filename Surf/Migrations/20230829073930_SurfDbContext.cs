using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Surf.Migrations
{
    public partial class SurfDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Surfboard",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Surfboard");
        }
    }
}
