﻿// <auto-generated />
using System;
using MVC_App.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVC_App.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240808155952_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CeyhunApplication.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Baslik");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Kategoriler", (string)null);
                });

            modelBuilder.Entity("CeyhunApplication.Models.PopularTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PopularTags");
                });

            modelBuilder.Entity("CeyhunApplication.Models.PopularTagPost", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("PopularTagId")
                        .HasColumnType("int");

                    b.HasKey("PostId", "PopularTagId");

                    b.HasIndex("PopularTagId");

                    b.ToTable("PopularTagPost");
                });

            modelBuilder.Entity("CeyhunApplication.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("CeyhunApplication.Models.Category", b =>
                {
                    b.HasOne("CeyhunApplication.Models.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("CeyhunApplication.Models.PopularTagPost", b =>
                {
                    b.HasOne("CeyhunApplication.Models.PopularTag", "PopularTag")
                        .WithMany("PopularTagPosts")
                        .HasForeignKey("PopularTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CeyhunApplication.Models.Post", "Post")
                        .WithMany("PostPopularTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PopularTag");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("CeyhunApplication.Models.Post", b =>
                {
                    b.HasOne("CeyhunApplication.Models.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("CeyhunApplication.Models.Category", b =>
                {
                    b.Navigation("Posts");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("CeyhunApplication.Models.PopularTag", b =>
                {
                    b.Navigation("PopularTagPosts");
                });

            modelBuilder.Entity("CeyhunApplication.Models.Post", b =>
                {
                    b.Navigation("PostPopularTags");
                });
#pragma warning restore 612, 618
        }
    }
}
