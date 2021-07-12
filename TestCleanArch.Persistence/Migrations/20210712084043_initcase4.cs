using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCleanArch.Persistence.Migrations
{
    public partial class initcase4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SendType",
                table: "Persons",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendType",
                table: "Persons");
        }
    }
}
