using Microsoft.EntityFrameworkCore.Migrations;

namespace JustChat.Migrations
{
    public partial class UserId_string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId1",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomUsers_AspNetUsers_UserId1",
                table: "RoomUsers");

            migrationBuilder.DropIndex(
                name: "IX_RoomUsers_UserId1",
                table: "RoomUsers");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AuthorId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "RoomUsers");

            migrationBuilder.DropColumn(
                name: "AuthorId1",
                table: "Messages");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RoomUsers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_RoomUsers_UserId",
                table: "RoomUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AuthorId",
                table: "Messages",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId",
                table: "Messages",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUsers_AspNetUsers_UserId",
                table: "RoomUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomUsers_AspNetUsers_UserId",
                table: "RoomUsers");

            migrationBuilder.DropIndex(
                name: "IX_RoomUsers_UserId",
                table: "RoomUsers");

            migrationBuilder.DropIndex(
                name: "IX_Messages_AuthorId",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RoomUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "RoomUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorId1",
                table: "Messages",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomUsers_UserId1",
                table: "RoomUsers",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_AuthorId1",
                table: "Messages",
                column: "AuthorId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_AuthorId1",
                table: "Messages",
                column: "AuthorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomUsers_AspNetUsers_UserId1",
                table: "RoomUsers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
