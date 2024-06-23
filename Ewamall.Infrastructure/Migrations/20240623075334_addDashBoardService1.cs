using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ewamall.Infrastructure.Migrations
{
    public partial class addDashBoardService1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Sellers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TotalDownloadLastMonth",
                table: "DashBoards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "TotalDownloadLastMonth",
                table: "DashBoards");
        }
    }
}
