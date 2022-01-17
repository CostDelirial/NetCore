﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SuspensionesAPI.Infraestructura.Repositories;

namespace SuspensionesAPI.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .IsRequired()
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

            modelBuilder.Entity("SuspensionesAPI.Core.Models.suspensiones", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("bls")
                        .HasColumnType("integer");

                    b.Property<int>("bph")
                        .HasColumnType("integer");

                    b.Property<int>("ductoId")
                        .HasColumnType("integer");

                    b.Property<string>("estatus")
                        .HasColumnType("text");

                    b.Property<DateTime>("fechaHora")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("km")
                        .HasColumnType("double precision");

                    b.Property<int>("motivoSuspensionId")
                        .HasColumnType("integer");

                    b.Property<string>("observaciones")
                        .HasColumnType("text");

                    b.Property<int>("personalCCId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("seregistro")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("id");

                    b.HasIndex("ductoId");

                    b.HasIndex("motivoSuspensionId");

                    b.HasIndex("personalCCId");

                    b.ToTable("suspensiones");
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

                    b.Property<int>("estatus")
                        .HasColumnType("integer");

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
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("cat_logistica");
                });

            modelBuilder.Entity("SuspensionesAPI.Core.Models.cat_motivoSuspension", b =>
                {
                    b.HasOne("suspensionesAPI.Core.Models.cat_logistica", "logistica")
                        .WithMany("cat_motivoSuspension")
                        .HasForeignKey("logisticaid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SuspensionesAPI.Core.Models.suspensiones", b =>
                {
                    b.HasOne("suspensionesAPI.Core.Models.cat_ducto", "ducto")
                        .WithMany("suspension")
                        .HasForeignKey("ductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuspensionesAPI.Core.Models.cat_motivoSuspension", "motivoSuspension")
                        .WithMany("suspension")
                        .HasForeignKey("motivoSuspensionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SuspensionesAPI.Core.Models.cat_personalCC", "personalCC")
                        .WithMany("suspension")
                        .HasForeignKey("personalCCId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
