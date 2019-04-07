﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PostService.Data;

namespace PostService.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190406184700_IncludeWallOwnerInPost")]
    partial class IncludeWallOwnerInPost
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PostService.Data.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AuthorId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<Guid?>("WallOwnerId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("WallOwnerId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("PostService.Data.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("ProfilePictureUrl");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PostService.Data.Post", b =>
                {
                    b.HasOne("PostService.Data.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("PostService.Data.User", "WallOwner")
                        .WithMany()
                        .HasForeignKey("WallOwnerId");
                });
#pragma warning restore 612, 618
        }
    }
}
