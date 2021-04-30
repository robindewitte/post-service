using Microsoft.EntityFrameworkCore.Migrations;

namespace twatter_postservice.Migrations
{
    public partial class AddPostHashTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HashTag",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashTag",
                table: "Posts");
        }
    }
}
