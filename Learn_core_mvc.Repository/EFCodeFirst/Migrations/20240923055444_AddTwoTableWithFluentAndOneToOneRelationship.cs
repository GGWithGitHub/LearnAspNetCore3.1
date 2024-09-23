using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn_core_mvc.Repository.EFCodeFirst.Migrations
{
    public partial class AddTwoTableWithFluentAndOneToOneRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblStudentFluentAPI",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Age = table.Column<int>(nullable: true),
                    IsRegularStudent = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblStudentFluentAPI", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblStudentDetailsFluentAPI",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    AdditionalInformation = table.Column<string>(nullable: true),
                    StudentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblStudentDetailsFluentAPI", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblStudentDetailsFluentAPI_TblStudentFluentAPI_StudentId",
                        column: x => x.StudentId,
                        principalTable: "TblStudentFluentAPI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblStudentDetailsFluentAPI_StudentId",
                table: "TblStudentDetailsFluentAPI",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_Name",
                table: "TblStudentFluentAPI",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblStudentDetailsFluentAPI");

            migrationBuilder.DropTable(
                name: "TblStudentFluentAPI");
        }
    }
}
