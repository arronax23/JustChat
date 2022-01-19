using Microsoft.EntityFrameworkCore.Migrations;

namespace JustChat.Migrations
{
    public partial class Add_MainRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Rooms ([Name]) values ('Main Room')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete Rooms where [Name] = 'Main Room'");
        }
    }
}
