﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonstersInc.Data;

namespace MonstersInc.Data.Migrations.IdentityServer.MonstersIncDb
{
    [DbContext(typeof(MonstersIncDBContext))]
    [Migration("20200814075815_InitialMonstersIncDbMigration")]
    partial class InitialMonstersIncDbMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MonstersInc.Core.Models.DepletedDoor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DoorId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("WorkDayPerformanceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DoorId");

                    b.HasIndex("WorkDayPerformanceId");

                    b.ToTable("DepletedDoors");
                });

            modelBuilder.Entity("MonstersInc.Core.Models.Door", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailableAmountOfEnergy")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doors");
                });

            modelBuilder.Entity("MonstersInc.Core.Models.WorkDayPerformance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActualEnergyAmount")
                        .HasColumnType("int");

                    b.Property<int>("ExpectedEnergyAmount")
                        .HasColumnType("int");

                    b.Property<Guid>("IntimidatorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("WorkDayDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("WorkDayPerformance");
                });

            modelBuilder.Entity("MonstersInc.Core.Models.DepletedDoor", b =>
                {
                    b.HasOne("MonstersInc.Core.Models.Door", "Door")
                        .WithMany()
                        .HasForeignKey("DoorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MonstersInc.Core.Models.WorkDayPerformance", null)
                        .WithMany("DepletedDoors")
                        .HasForeignKey("WorkDayPerformanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
