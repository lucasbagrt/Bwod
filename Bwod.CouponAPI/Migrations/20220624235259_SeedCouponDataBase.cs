using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bwod.CouponAPI.Migrations
{
    public partial class SeedCouponDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupon",
                columns: new[] { "id", "coupon_code", "discount_amount" },
                values: new object[] { 1, "UNIAMERICA", 10m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupon",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
