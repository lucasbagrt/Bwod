﻿// <auto-generated />
using Bwod.CouponAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bwod.CouponAPI.Migrations
{
    [DbContext(typeof(MySQLContext))]
    [Migration("20220624235259_SeedCouponDataBase")]
    partial class SeedCouponDataBase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Bwod.CouponAPI.Model.Coupon", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("coupon_code")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("coupon_code");

                    b.Property<decimal>("discount_amount")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("discount_amount");

                    b.HasKey("id");

                    b.ToTable("Coupon");

                    b.HasData(
                        new
                        {
                            id = 1,
                            coupon_code = "UNIAMERICA",
                            discount_amount = 10m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
