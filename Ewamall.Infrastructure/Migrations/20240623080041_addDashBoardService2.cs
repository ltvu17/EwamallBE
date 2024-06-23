using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ewamall.Infrastructure.Migrations
{
    public partial class addDashBoardService2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalDownloadLastMonth",
                table: "DashBoards",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "TotalDownload",
                table: "DashBoards",
                newName: "Month");

            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "DashBoards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "DashBoards");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "DashBoards",
                newName: "TotalDownloadLastMonth");

            migrationBuilder.RenameColumn(
                name: "Month",
                table: "DashBoards",
                newName: "TotalDownload");
        }
    }
}
