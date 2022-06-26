﻿// <auto-generated />
using System;
using Bwod.Email.Model.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bwod.Email.Migrations
{
    [DbContext(typeof(MySQLContext))]
    [Migration("20220626040155_AddEmailDataTableOnDB")]
    partial class AddEmailDataTableOnDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Bwod.Email.Model.EmailLog", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("email")
                        .HasColumnType("longtext")
                        .HasColumnName("email");

                    b.Property<string>("log")
                        .HasColumnType("longtext")
                        .HasColumnName("log");

                    b.Property<DateTime>("send_date")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("sent_date");

                    b.HasKey("id");

                    b.ToTable("email_logs");
                });
#pragma warning restore 612, 618
        }
    }
}
