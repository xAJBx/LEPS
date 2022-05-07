using Microsoft.EntityFrameworkCore.Migrations;

namespace LEPS.Migrations
{
    public partial class enrollmentstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "EventEnrollments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "EventEnrollments");
        }
    }
}
