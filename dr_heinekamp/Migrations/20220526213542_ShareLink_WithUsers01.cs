using Microsoft.EntityFrameworkCore.Migrations;

namespace dr_heinekamp.Migrations
{
    public partial class ShareLink_WithUsers01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareLink_WithUsers_AspNetUsers_UserId",
                table: "ShareLink_WithUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ShareLink_WithUsers",
                newName: "ShareId");

            migrationBuilder.RenameIndex(
                name: "IX_ShareLink_WithUsers_UserId",
                table: "ShareLink_WithUsers",
                newName: "IX_ShareLink_WithUsers_ShareId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareLink_WithUsers_AspNetUsers_ShareId",
                table: "ShareLink_WithUsers",
                column: "ShareId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShareLink_WithUsers_AspNetUsers_ShareId",
                table: "ShareLink_WithUsers");

            migrationBuilder.RenameColumn(
                name: "ShareId",
                table: "ShareLink_WithUsers",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShareLink_WithUsers_ShareId",
                table: "ShareLink_WithUsers",
                newName: "IX_ShareLink_WithUsers_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShareLink_WithUsers_AspNetUsers_UserId",
                table: "ShareLink_WithUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
