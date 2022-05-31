using Microsoft.EntityFrameworkCore.Migrations;

namespace dr_heinekamp.Migrations
{
    public partial class SubFiles01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "SubFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "SubFiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "SubFiles");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "SubFiles");
        }
    }
}
