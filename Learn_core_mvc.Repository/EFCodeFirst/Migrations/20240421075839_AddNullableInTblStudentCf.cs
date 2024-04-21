using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn_core_mvc.Repository.EFCodeFirst.Migrations
{
    public partial class AddNullableInTblStudentCf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblStudentCf_TblCourseCf_CourseId",
                table: "TblStudentCf");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "TblStudentCf",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TblStudentCf_TblCourseCf_CourseId",
                table: "TblStudentCf",
                column: "CourseId",
                principalTable: "TblCourseCf",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblStudentCf_TblCourseCf_CourseId",
                table: "TblStudentCf");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "TblStudentCf",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TblStudentCf_TblCourseCf_CourseId",
                table: "TblStudentCf",
                column: "CourseId",
                principalTable: "TblCourseCf",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
