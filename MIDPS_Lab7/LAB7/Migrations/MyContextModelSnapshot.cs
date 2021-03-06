﻿// <auto-generated />
using LAB7.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace LAB7.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("LAB7.Models.Ring", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Material");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Ring");
                });

            modelBuilder.Entity("LAB7.Models.Wizard", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Wizard");
                });

            modelBuilder.Entity("LAB7.Models.WizardRing", b =>
                {
                    b.Property<int>("WizardID");

                    b.Property<int>("RingID");

                    b.HasKey("WizardID", "RingID");

                    b.HasIndex("RingID");

                    b.ToTable("WizardRing");
                });

            modelBuilder.Entity("LAB7.Models.WizardRing", b =>
                {
                    b.HasOne("LAB7.Models.Ring", "Ring")
                        .WithMany("WizardRings")
                        .HasForeignKey("RingID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LAB7.Models.Wizard", "Wizard")
                        .WithMany("WizardRings")
                        .HasForeignKey("WizardID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
