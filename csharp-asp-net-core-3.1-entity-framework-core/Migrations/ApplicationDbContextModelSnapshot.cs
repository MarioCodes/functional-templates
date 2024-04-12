﻿// <auto-generated />
using System;
using EntityFramework.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EntityFramework.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EntityFramework.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("MostrarOrden")
                        .HasColumnType("integer");

                    b.Property<string>("NombreCategoria")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("EntityFramework.Models.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("integer");

                    b.Property<string>("DescripcionCorta")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("DescripcionLarga")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Precio")
                        .HasColumnType("double precision");

                    b.Property<int?>("TipoAplicacion")
                        .HasColumnType("integer");

                    b.Property<int>("TipoAplicacionId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("TipoAplicacion");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("EntityFramework.Models.TipoAplicacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TipoAplicacion");
                });

            modelBuilder.Entity("EntityFramework.Models.Producto", b =>
                {
                    b.HasOne("EntityFramework.Models.Categoria", "categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFramework.Models.TipoAplicacion", "tipoAplicacion")
                        .WithMany()
                        .HasForeignKey("TipoAplicacion");

                    b.Navigation("categoria");

                    b.Navigation("tipoAplicacion");
                });
#pragma warning restore 612, 618
        }
    }
}
