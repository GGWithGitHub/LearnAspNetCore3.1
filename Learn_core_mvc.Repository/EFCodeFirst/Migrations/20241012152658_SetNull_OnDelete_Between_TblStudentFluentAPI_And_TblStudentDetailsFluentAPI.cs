using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn_core_mvc.Repository.EFCodeFirst.Migrations
{
    public partial class SetNull_OnDelete_Between_TblStudentFluentAPI_And_TblStudentDetailsFluentAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblStudentDetailsFluentAPI_TblStudentFluentAPI_StudentId",
                table: "TblStudentDetailsFluentAPI");

            migrationBuilder.DropIndex(
                name: "IX_TblStudentDetailsFluentAPI_StudentId",
                table: "TblStudentDetailsFluentAPI");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "TblStudentDetailsFluentAPI",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_TblStudentDetailsFluentAPI_StudentId",
                table: "TblStudentDetailsFluentAPI",
                column: "StudentId",
                unique: true,
                filter: "[StudentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_TblStudentDetailsFluentAPI_TblStudentFluentAPI_StudentId",
                table: "TblStudentDetailsFluentAPI",
                column: "StudentId",
                principalTable: "TblStudentFluentAPI",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TblStudentDetailsFluentAPI_TblStudentFluentAPI_StudentId",
                table: "TblStudentDetailsFluentAPI");

            migrationBuilder.DropIndex(
                name: "IX_TblStudentDetailsFluentAPI_StudentId",
                table: "TblStudentDetailsFluentAPI");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "TblStudentDetailsFluentAPI",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TblStudentDetailsFluentAPI_StudentId",
                table: "TblStudentDetailsFluentAPI",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TblStudentDetailsFluentAPI_TblStudentFluentAPI_StudentId",
                table: "TblStudentDetailsFluentAPI",
                column: "StudentId",
                principalTable: "TblStudentFluentAPI",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
