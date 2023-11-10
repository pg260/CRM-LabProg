﻿// <auto-generated />
using System;
using CRM.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CRM.Infra.Migrations
{
    [DbContext(typeof(BaseDbContext))]
    partial class BaseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");
            MySqlModelBuilderExtensions.UseGuidCollation(modelBuilder, "");

            modelBuilder.Entity("CRM.Domain.Entities.Carrinho", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<float>("ValorTotal")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Carrinhos");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Compra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime");

                    b.Property<int>("HistoricoComprasId")
                        .HasColumnType("int");

                    b.Property<int>("HistoricoId")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<float>("ValorTotal")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("HistoricoComprasId");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("UserId");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("datetime");

                    b.Property<int>("Avaliacao")
                        .HasColumnType("int");

                    b.Property<string>("Comentarios")
                        .HasColumnType("longtext");

                    b.Property<int>("CompraId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CompraId");

                    b.HasIndex("ProdutoId");

                    b.HasIndex("UserId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("CRM.Domain.Entities.HistoricoCompras", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<float>("ValorHistorico")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("HistoricoCompras");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("datetime");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime");

                    b.Property<bool>("Desativado")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<float>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("CRM.Domain.Entities.ProdutoCarrinho", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("datetime");

                    b.Property<int>("CarrinhoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CarrinhoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ProdutoCarrinhos");
                });

            modelBuilder.Entity("CRM.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<byte[]>("Foto")
                        .HasColumnType("longblob");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Carrinho", b =>
                {
                    b.HasOne("CRM.Domain.Entities.User", "Usuario")
                        .WithOne("Carrinho")
                        .HasForeignKey("CRM.Domain.Entities.Carrinho", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Compra", b =>
                {
                    b.HasOne("CRM.Domain.Entities.HistoricoCompras", "HistoricoCompras")
                        .WithMany("Compras")
                        .HasForeignKey("HistoricoComprasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Domain.Entities.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Domain.Entities.User", "User")
                        .WithMany("Compras")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HistoricoCompras");

                    b.Navigation("Produto");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Feedback", b =>
                {
                    b.HasOne("CRM.Domain.Entities.Compra", "Compra")
                        .WithMany("Feedbacks")
                        .HasForeignKey("CompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Domain.Entities.Produto", "Produto")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Domain.Entities.User", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("Produto");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CRM.Domain.Entities.HistoricoCompras", b =>
                {
                    b.HasOne("CRM.Domain.Entities.User", "User")
                        .WithMany("HistoricoCompras")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Produto", b =>
                {
                    b.HasOne("CRM.Domain.Entities.User", "User")
                        .WithMany("Produtos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CRM.Domain.Entities.ProdutoCarrinho", b =>
                {
                    b.HasOne("CRM.Domain.Entities.Carrinho", "Carrinho")
                        .WithMany("ProdutoCarrinhos")
                        .HasForeignKey("CarrinhoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CRM.Domain.Entities.Produto", "Produto")
                        .WithMany("ProdutoCarrinhos")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrinho");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Carrinho", b =>
                {
                    b.Navigation("ProdutoCarrinhos");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Compra", b =>
                {
                    b.Navigation("Feedbacks");
                });

            modelBuilder.Entity("CRM.Domain.Entities.HistoricoCompras", b =>
                {
                    b.Navigation("Compras");
                });

            modelBuilder.Entity("CRM.Domain.Entities.Produto", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("ProdutoCarrinhos");
                });

            modelBuilder.Entity("CRM.Domain.Entities.User", b =>
                {
                    b.Navigation("Carrinho");

                    b.Navigation("Compras");

                    b.Navigation("Feedbacks");

                    b.Navigation("HistoricoCompras");

                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}
