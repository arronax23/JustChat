using Microsoft.EntityFrameworkCore.Migrations;

namespace JustChat.Migrations
{
    public partial class add_ConnectionPair : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConnectionPairs",
                columns: table => new
                {
                    ConnectionPairId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConnectionIdPaired = table.Column<int>(type: "int", nullable: false),
                    IsPaired = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionPairs", x => x.ConnectionPairId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectionPairs");
        }
    }
}
