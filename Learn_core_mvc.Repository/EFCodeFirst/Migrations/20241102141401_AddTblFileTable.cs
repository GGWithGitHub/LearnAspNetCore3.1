using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Learn_core_mvc.Repository.EFCodeFirst.Migrations
{
    public partial class AddTblFileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblFile",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    MimeType = table.Column<string>(nullable: true),
                    Content = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblFile", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblFile");
        }
    }
}
