﻿// <auto-generated />
using System;
using IdentityService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IdentityService.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190331182926_AddFriendsToUser")]
    partial class AddFriendsToUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("IdentityService.Data.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("LastName");

                    b.Property<DateTime?>("ModifiedAt");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IdentityService.Data.UserFriend", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("FriendId");

                    b.HasKey("UserId", "FriendId");

                    b.HasAlternateKey("FriendId", "UserId");

                    b.ToTable("UserFriends");
                });

            modelBuilder.Entity("IdentityService.Data.UserFriend", b =>
                {
                    b.HasOne("IdentityService.Data.User")
                        .WithMany("Friends")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
