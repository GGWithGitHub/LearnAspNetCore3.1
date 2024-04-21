using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn_core_mvc.Repository.EFCodeFirst.Migrations
{
    public partial class Added_DeleteCascade_In_TblStudentCf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblStudentCf_TblCourseCf_CourseId",
                table: "TblStudentCf");

            migrationBuilder.AddForeignKey(
                name: "FK_TblStudentCf_TblCourseCf_CourseId",
                table: "TblStudentCf",
                column: "CourseId",
                principalTable: "TblCourseCf",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblStudentCf_TblCourseCf_CourseId",
                table: "TblStudentCf");

            migrationBuilder.AddForeignKey(
                name: "FK_TblStudentCf_TblCourseCf_CourseId",
                table: "TblStudentCf",
                column: "CourseId",
                principalTable: "TblCourseCf",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
