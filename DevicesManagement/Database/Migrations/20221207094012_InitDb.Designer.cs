﻿// <auto-generated />
using System;
using Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Database.Migrations
{
    [DbContext(typeof(DeviceManagementContext))]
    [Migration("20221207094012_InitDb")]
    partial class InitDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Database.Models.Command", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid?>("DeviceId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("Database.Models.CommandHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CommandId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("DeviceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CommandId");

                    b.HasIndex("DeviceId");

                    b.ToTable("DevicesCommandHistory");
                });

            modelBuilder.Entity("Database.Models.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("EmployeeId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("Database.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("DeviceId")
                        .HasColumnType("uuid");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.ToTable("DevicesMessageHistory");
                });

            modelBuilder.Entity("Database.Models.Command", b =>
                {
                    b.HasOne("Database.Models.Device", null)
                        .WithMany("Commands")
                        .HasForeignKey("DeviceId");
                });

            modelBuilder.Entity("Database.Models.CommandHistory", b =>
                {
                    b.HasOne("Database.Models.Command", "Command")
                        .WithMany()
                        .HasForeignKey("CommandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Database.Models.Device", null)
                        .WithMany("CommandHistory")
                        .HasForeignKey("DeviceId");

                    b.Navigation("Command");
                });

            modelBuilder.Entity("Database.Models.Message", b =>
                {
                    b.HasOne("Database.Models.Device", null)
                        .WithMany("MessageHistory")
                        .HasForeignKey("DeviceId");
                });

            modelBuilder.Entity("Database.Models.Device", b =>
                {
                    b.Navigation("CommandHistory");

                    b.Navigation("Commands");

                    b.Navigation("MessageHistory");
                });
#pragma warning restore 612, 618
        }
    }
}