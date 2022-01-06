﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SuspensionesAPI.Infraestructura.Repositories;

namespace SuspensionesAPI.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20220105190145_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("SuspensionesAPI.Core.Models.cat_motivoSuspension", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("logisticaid")
                        .HasColumnType("integer");

                    b.Property<string>("nombre")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("logisticaid");

                    b.ToTable("cat_motivoSuspension");
                });

            modelBuilder.Entity("SuspensionesAPI.Core.Models.cat_personalCC", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("nombre")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("cat_personalCC");
                });

            modelBuilder.Entity("SuspensionesAPI.Core.Models.usuarios", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("control")
                        .HasColumnType("integer");

                    b.Property<int>("estatus")
                        .HasColumnType("integer");

                    b.Property<string>("nombre")
                        .HasColumnType("text");

                    b.Property<string>("password")
                        .HasColumnType("text");

                    b.Property<int>("tipo")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("usuarios");
                });

            modelBuilder.Entity("suspensionesAPI.Core.Models.cat_ducto", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("cat_ducto");
                });

            modelBuilder.Entity("suspensionesAPI.Core.Models.cat_logistica", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("nombre")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("cat_logistica");
                });

            modelBuilder.Entity("SuspensionesAPI.Core.Models.cat_motivoSuspension", b =>
                {
                    b.HasOne("suspensionesAPI.Core.Models.cat_logistica", "logistica")
                        .WithMany("cat_motivoSuspensiones")
                        .HasForeignKey("logisticaid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
