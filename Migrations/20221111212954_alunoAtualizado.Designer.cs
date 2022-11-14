﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Universidade_Api;

#nullable disable

namespace Universidade_Api.Migrations
{
    [DbContext(typeof(UniversidadeContext))]
    [Migration("20221111212954_alunoAtualizado")]
    partial class alunoAtualizado
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Universidade_Api.Aluno", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("CursoId")
                        .HasColumnType("bigint");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<long?>("Saldo")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.ToTable("Alunos");
                });

            modelBuilder.Entity("Universidade_Api.Curso", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("Sigla")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("Universidade_Api.Nota", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("AlunoId")
                        .HasColumnType("bigint");

                    b.Property<long?>("UnidadeCurricularId")
                        .HasColumnType("bigint");

                    b.Property<double>("Valor")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("UnidadeCurricularId");

                    b.ToTable("Notas");
                });

            modelBuilder.Entity("Universidade_Api.UnidadeCurricular", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("AlunoId")
                        .HasColumnType("bigint");

                    b.Property<int?>("Ano")
                        .HasColumnType("int");

                    b.Property<long?>("CursoId")
                        .HasColumnType("bigint");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext");

                    b.Property<string>("Sigla")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AlunoId");

                    b.HasIndex("CursoId");

                    b.ToTable("UnidadesCurriculares");
                });

            modelBuilder.Entity("Universidade_Api.Aluno", b =>
                {
                    b.HasOne("Universidade_Api.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId");

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("Universidade_Api.Nota", b =>
                {
                    b.HasOne("Universidade_Api.Aluno", "Aluno")
                        .WithMany()
                        .HasForeignKey("AlunoId");

                    b.HasOne("Universidade_Api.UnidadeCurricular", "UnidadeCurricular")
                        .WithMany()
                        .HasForeignKey("UnidadeCurricularId");

                    b.Navigation("Aluno");

                    b.Navigation("UnidadeCurricular");
                });

            modelBuilder.Entity("Universidade_Api.UnidadeCurricular", b =>
                {
                    b.HasOne("Universidade_Api.Aluno", null)
                        .WithMany("UnidadesCurriculares")
                        .HasForeignKey("AlunoId");

                    b.HasOne("Universidade_Api.Curso", "Curso")
                        .WithMany()
                        .HasForeignKey("CursoId");

                    b.Navigation("Curso");
                });

            modelBuilder.Entity("Universidade_Api.Aluno", b =>
                {
                    b.Navigation("UnidadesCurriculares");
                });
#pragma warning restore 612, 618
        }
    }
}