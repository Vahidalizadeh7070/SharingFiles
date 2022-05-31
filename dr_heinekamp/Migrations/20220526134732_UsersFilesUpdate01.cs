using Microsoft.EntityFrameworkCore.Migrations;

namespace dr_heinekamp.Migrations
{
    public partial class UsersFilesUpdate01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Files",
                table: "UserFiles",
                newName: "File");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserFiles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFiles_UserId",
                table: "UserFiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserFiles_AspNetUsers_UserId",
                table: "UserFiles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFiles_AspNetUsers_UserId",
                table: "UserFiles");

            migrationBuilder.DropIndex(
                name: "IX_UserFiles_UserId",
                table: "UserFiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserFiles");

            migrationBuilder.RenameColumn(
                name: "File",
                table: "UserFiles",
                newName: "Files");
        }
    }
}
