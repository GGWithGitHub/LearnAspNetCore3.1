using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn_core_mvc.Repository.EFCodeFirst.Migrations
{
    public partial class AddTableWithFluentAndOneToManyRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblEvaluationFluentAPI",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Grade = table.Column<int>(nullable: false),
                    AdditionalExplanation = table.Column<string>(nullable: true),
                    StudentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblEvaluationFluentAPI", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblEvaluationFluentAPI_TblStudentFluentAPI_StudentId",
                        column: x => x.StudentId,
                        principalTable: "TblStudentFluentAPI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblEvaluationFluentAPI_StudentId",
                table: "TblEvaluationFluentAPI",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblEvaluationFluentAPI");
        }
    }
}
