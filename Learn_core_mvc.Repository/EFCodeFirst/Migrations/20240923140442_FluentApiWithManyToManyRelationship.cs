using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn_core_mvc.Repository.EFCodeFirst.Migrations
{
    public partial class FluentApiWithManyToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblSubjectFluentAPI",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SubjectName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblSubjectFluentAPI", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblStudentSubjectFluentAPI",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(nullable: false),
                    SubjectId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblStudentSubjectFluentAPI", x => new { x.StudentId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_TblStudentSubjectFluentAPI_TblStudentFluentAPI_StudentId",
                        column: x => x.StudentId,
                        principalTable: "TblStudentFluentAPI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblStudentSubjectFluentAPI_TblSubjectFluentAPI_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "TblSubjectFluentAPI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblStudentSubjectFluentAPI_SubjectId",
                table: "TblStudentSubjectFluentAPI",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblStudentSubjectFluentAPI");

            migrationBuilder.DropTable(
                name: "TblSubjectFluentAPI");
        }
    }
}
