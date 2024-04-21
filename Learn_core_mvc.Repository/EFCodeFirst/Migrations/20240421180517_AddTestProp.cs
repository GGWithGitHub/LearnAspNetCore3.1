using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn_core_mvc.Repository.EFCodeFirst.Migrations
{
    public partial class AddTestProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Test",
                table: "TblStudentCf",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Test",
                table: "TblStudentCf");
        }
    }
}
