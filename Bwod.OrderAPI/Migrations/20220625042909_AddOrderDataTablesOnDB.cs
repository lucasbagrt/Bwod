﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bwod.OrderAPI.Migrations
{
    public partial class AddOrderDataTablesOnDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order_header",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    coupon_code = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    purchase_amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    first_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    last_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    purchase_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    order_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    phone_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    card_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cvv = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    expiry_month_year = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    total_itens = table.Column<int>(type: "int", nullable: false),
                    payment_status = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_header", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order_detail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    order_header_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    product_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_detail_order_header_order_header_id",
                        column: x => x.order_header_id,
                        principalTable: "order_header",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_order_detail_order_header_id",
                table: "order_detail",
                column: "order_header_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_detail");

            migrationBuilder.DropTable(
                name: "order_header");
        }
    }
}
