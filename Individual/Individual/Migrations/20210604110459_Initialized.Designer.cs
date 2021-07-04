﻿// <auto-generated />
using System;
using Individual.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Individual.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210604110459_Initialized")]
    partial class Initialized
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Individual.Models.Entities.Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("Individual.Models.Entities.Borrower", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AmountRequested")
                        .HasColumnType("int");

                    b.Property<int?>("BankId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasportNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("Borrowers");
                });

            modelBuilder.Entity("Individual.Models.Entities.CreditType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BankId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BankId");

                    b.ToTable("CreditTypes");
                });

            modelBuilder.Entity("Individual.Models.Entities.Guarantor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BorrowerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasportNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BorrowerId");

                    b.ToTable("Guarantors");
                });

            modelBuilder.Entity("Individual.Models.Entities.Borrower", b =>
                {
                    b.HasOne("Individual.Models.Entities.Bank", "Bank")
                        .WithMany("Borrowers")
                        .HasForeignKey("BankId");

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("Individual.Models.Entities.CreditType", b =>
                {
                    b.HasOne("Individual.Models.Entities.Bank", "Bank")
                        .WithMany("CreditTypes")
                        .HasForeignKey("BankId");

                    b.Navigation("Bank");
                });

            modelBuilder.Entity("Individual.Models.Entities.Guarantor", b =>
                {
                    b.HasOne("Individual.Models.Entities.Borrower", "Borrower")
                        .WithMany("Guarantors")
                        .HasForeignKey("BorrowerId");

                    b.Navigation("Borrower");
                });

            modelBuilder.Entity("Individual.Models.Entities.Bank", b =>
                {
                    b.Navigation("Borrowers");

                    b.Navigation("CreditTypes");
                });

            modelBuilder.Entity("Individual.Models.Entities.Borrower", b =>
                {
                    b.Navigation("Guarantors");
                });
#pragma warning restore 612, 618
        }
    }
}
