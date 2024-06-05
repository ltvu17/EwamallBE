using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ewamall.Infrastructure.Migrations
{
    public partial class UpdateSellerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Sellers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "Sellers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WardId",
                table: "Sellers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "Sellers");
        }
    }
}
