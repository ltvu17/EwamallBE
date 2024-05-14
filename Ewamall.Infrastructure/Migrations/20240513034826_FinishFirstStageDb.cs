using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ewamall.Infrastructure.Migrations
{
    public partial class FinishFirstStageDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_ProductDetail_ProductDetailId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Payments_PaymentId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSellDetails_ProductDetail_ParentNodeId",
                table: "ProductSellDetails");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "ProductDetailId",
                table: "OrderDetails",
                newName: "ProductSellDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductDetailId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductSellDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_ProductSellDetails_ProductSellDetailId",
                table: "OrderDetails",
                column: "ProductSellDetailId",
                principalTable: "ProductSellDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSellDetails_ProductSellDetails_ParentNodeId",
                table: "ProductSellDetails",
                column: "ParentNodeId",
                principalTable: "ProductSellDetails",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_ProductSellDetails_ProductSellDetailId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSellDetails_ProductSellDetails_ParentNodeId",
                table: "ProductSellDetails");

            migrationBuilder.RenameColumn(
                name: "ProductSellDetailId",
                table: "OrderDetails",
                newName: "ProductDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductSellDetailId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductDetailId");

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentId",
                table: "Payments",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_ProductDetail_ProductDetailId",
                table: "OrderDetails",
                column: "ProductDetailId",
                principalTable: "ProductDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Payments_PaymentId",
                table: "Payments",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSellDetails_ProductDetail_ParentNodeId",
                table: "ProductSellDetails",
                column: "ParentNodeId",
                principalTable: "ProductDetail",
                principalColumn: "Id");
        }
    }
}
