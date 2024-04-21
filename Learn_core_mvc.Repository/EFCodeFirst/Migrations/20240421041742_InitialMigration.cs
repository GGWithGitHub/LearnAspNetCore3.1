using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn_core_mvc.Repository.EFCodeFirst.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblCourseCf",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCourseCf", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "TblStudentCf",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    EmailId = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblStudentCf", x => x.StudentId);
                    table.ForeignKey(
                        name: "FK_TblStudentCf_TblCourseCf_CourseId",
                        column: x => x.CourseId,
                        principalTable: "TblCourseCf",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblStudentCf_CourseId",
                table: "TblStudentCf",
                column: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblStudentCf");

            migrationBuilder.DropTable(
                name: "TblCourseCf");
        }
    }
}
