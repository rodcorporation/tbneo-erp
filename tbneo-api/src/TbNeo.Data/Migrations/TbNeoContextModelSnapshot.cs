﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TbNeo.Data;

namespace TbNeo.Data.Migrations
{
    [DbContext(typeof(TbNeoContext))]
    partial class TbNeoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TbNeo.Domain.Entities.FeatureFlag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AtualizadoEm")
                        .HasColumnType("datetime2");

                    b.Property<int?>("AtualizadoPorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CriadoPorId")
                        .HasColumnType("int");

                    b.Property<int?>("IdAtualizadoPor")
                        .HasColumnType("int");

                    b.Property<int>("IdCriadoPor")
                        .HasColumnType("int");

                    b.Property<Guid>("IdLogReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("IdProjeto")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AtualizadoPorId");

                    b.HasIndex("CriadoPorId");

                    b.HasIndex("IdProjeto");

                    b.ToTable("FeatureFlag");
                });

            modelBuilder.Entity("TbNeo.Domain.Entities.LogSistema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AlteradoEm")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdAlteradoPor")
                        .HasColumnType("int");

                    b.Property<Guid>("IdLogReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NomeCampo")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("Reference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ValorAntigo")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ValorNovo")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("IdAlteradoPor");

                    b.ToTable("LogSistema");
                });

            modelBuilder.Entity("TbNeo.Domain.Entities.Projeto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("AtualizadoEm")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("varchar(500)");

                    b.Property<int?>("IdAtualizadoPor")
                        .HasColumnType("int");

                    b.Property<int>("IdCriadoPor")
                        .HasColumnType("int");

                    b.Property<Guid>("IdLogReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UrlJira")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("IdAtualizadoPor");

                    b.HasIndex("IdCriadoPor");

                    b.ToTable("Projeto");
                });

            modelBuilder.Entity("TbNeo.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<Guid>("IdLogReference")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("TbNeo.Domain.Entities.FeatureFlag", b =>
                {
                    b.HasOne("TbNeo.Domain.Entities.Usuario", "AtualizadoPor")
                        .WithMany()
                        .HasForeignKey("AtualizadoPorId");

                    b.HasOne("TbNeo.Domain.Entities.Usuario", "CriadoPor")
                        .WithMany()
                        .HasForeignKey("CriadoPorId");

                    b.HasOne("TbNeo.Domain.Entities.Projeto", "Projeto")
                        .WithMany("FeatureFlags")
                        .HasForeignKey("IdProjeto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AtualizadoPor");

                    b.Navigation("CriadoPor");

                    b.Navigation("Projeto");
                });

            modelBuilder.Entity("TbNeo.Domain.Entities.LogSistema", b =>
                {
                    b.HasOne("TbNeo.Domain.Entities.Usuario", "AlteradoPor")
                        .WithMany()
                        .HasForeignKey("IdAlteradoPor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AlteradoPor");
                });

            modelBuilder.Entity("TbNeo.Domain.Entities.Projeto", b =>
                {
                    b.HasOne("TbNeo.Domain.Entities.Usuario", "AtualizadoPor")
                        .WithMany()
                        .HasForeignKey("IdAtualizadoPor");

                    b.HasOne("TbNeo.Domain.Entities.Usuario", "CriadoPor")
                        .WithMany()
                        .HasForeignKey("IdCriadoPor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AtualizadoPor");

                    b.Navigation("CriadoPor");
                });

            modelBuilder.Entity("TbNeo.Domain.Entities.Projeto", b =>
                {
                    b.Navigation("FeatureFlags");
                });
#pragma warning restore 612, 618
        }
    }
}
