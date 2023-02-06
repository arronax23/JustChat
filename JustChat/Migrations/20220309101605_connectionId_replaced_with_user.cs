using Microsoft.EntityFrameworkCore.Migrations;

namespace JustChat.Migrations
{
    public partial class connectionId_replaced_with_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConnectionId",
                table: "AnonymousConnections");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AnonymousConnections",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousConnections_UserId",
                table: "AnonymousConnections",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonymousConnections_AspNetUsers_UserId",
                table: "AnonymousConnections",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonymousConnections_AspNetUsers_UserId",
                table: "AnonymousConnections");

            migrationBuilder.DropIndex(
                name: "IX_AnonymousConnections_UserId",
                table: "AnonymousConnections");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AnonymousConnections");

            migrationBuilder.AddColumn<string>(
                name: "ConnectionId",
                table: "AnonymousConnections",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
