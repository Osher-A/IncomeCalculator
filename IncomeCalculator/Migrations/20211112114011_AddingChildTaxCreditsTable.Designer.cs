﻿// <auto-generated />
using System;
using IncomeCalculator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IncomeCalculator.Migrations
{
    [DbContext(typeof(BenefitsContext))]
    [Migration("20211112114011_AddingChildTaxCreditsTable")]
    partial class AddingChildTaxCreditsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "Latin1_General_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IncomeCalculator.Data.ChildTaxCredit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("ChildElement")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("FamilyElement")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TaxYear")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Threshold")
                        .HasColumnType("decimal(18,2)");

                    b.Property<float>("WithdrawalRate")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("ChildTaxCredits");
                });

            modelBuilder.Entity("IncomeCalculator.Data.MinWage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("TaxYear")
                        .HasColumnType("date");

                    b.Property<decimal>("Wage")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("MinWages");
                });
#pragma warning restore 612, 618
        }
    }
}
