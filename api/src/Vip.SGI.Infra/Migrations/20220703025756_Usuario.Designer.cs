﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vip.SGI.Infra.Context;

#nullable disable

namespace Vip.SGI.Infra.Migrations
{
    [DbContext(typeof(SgiContext))]
    [Migration("20220703025756_Usuario")]
    partial class Usuario
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Vip.SGI.Domain.Models.Usuario", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("UsuarioFuncao")
                        .HasColumnType("int");

                    b.HasKey("UsuarioId");

                    b.HasIndex("Nome")
                        .HasDatabaseName("IX_Usuario_Nome");

                    b.ToTable("Usuario", (string)null);
                });

            modelBuilder.Entity("Vip.SGI.Domain.Models.Usuario", b =>
                {
                    b.OwnsOne("Vip.SGI.Domain.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UsuarioId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Endereco")
                                .IsRequired()
                                .HasMaxLength(256)
                                .HasColumnType("varchar(256)")
                                .HasColumnName("Email");

                            b1.HasKey("UsuarioId");

                            b1.HasIndex("Endereco")
                                .HasDatabaseName("IX_Usuario_Email");

                            b1.ToTable("Usuario");

                            b1.WithOwner()
                                .HasForeignKey("UsuarioId");
                        });

                    b.Navigation("Email")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}