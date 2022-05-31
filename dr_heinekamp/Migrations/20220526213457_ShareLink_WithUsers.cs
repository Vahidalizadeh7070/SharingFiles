using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dr_heinekamp.Migrations
{
    public partial class ShareLink_WithUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShareLink_WithUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShareUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserFilesId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DownloadURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Download = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareLink_WithUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShareLink_WithUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ShareLink_WithUsers_UserFiles_UserFilesId",
                        column: x => x.UserFilesId,
                        principalTable: "UserFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShareLink_WithUsers_UserFilesId",
                table: "ShareLink_WithUsers",
                column: "UserFilesId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareLink_WithUsers_UserId",
                table: "ShareLink_WithUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShareLink_WithUsers");
        }
    }
}
