﻿// <auto-generated />
using System;
using ConsoleApp3;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ConsoleApp3.Migrations
{
    [DbContext(typeof(RackContext))]
    [Migration("20210129023000_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ConsoleApp3.Column", b =>
                {
                    b.Property<int>("ColumnID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.HasKey("ColumnID");

                    b.ToTable("Columns");
                });

            modelBuilder.Entity("ConsoleApp3.Shelf", b =>
                {
                    b.Property<int>("ShelfID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Barcode")
                        .HasColumnType("text");

                    b.Property<int?>("ColumnID")
                        .HasColumnType("integer");

                    b.Property<int>("Theta")
                        .HasColumnType("integer");

                    b.Property<int?>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("ShelfID");

                    b.HasIndex("ColumnID");

                    b.HasIndex("TypeId");

                    b.ToTable("Shelfs");
                });

            modelBuilder.Entity("ConsoleApp3.ShelfType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .UseIdentityByDefaultColumn();

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ShelfType");
                });

            modelBuilder.Entity("ConsoleApp3.Shelf", b =>
                {
                    b.HasOne("ConsoleApp3.Column", "column")
                        .WithMany("Shelfs")
                        .HasForeignKey("ColumnID");

                    b.HasOne("ConsoleApp3.ShelfType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("column");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("ConsoleApp3.Column", b =>
                {
                    b.Navigation("Shelfs");
                });
#pragma warning restore 612, 618
        }
    }
}