using Microsoft.EntityFrameworkCore.Migrations;

namespace TestCleanArch.Persistence.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientIpMismatchs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ip = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIpMismatchs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientIpMismatchs_Ip",
                table: "ClientIpMismatchs",
                column: "Ip");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientIpMismatchs");
        }
    }
}
