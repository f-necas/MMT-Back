﻿// <auto-generated />
using System;
using MMT_Back;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MMT_Back.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211004213536_UpdateInvitationModel")]
    partial class UpdateInvitationModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0-rc.1.21452.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MMT_Back.EntityModels.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("BecameFriendsTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("FriendRequestFlag")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RequestTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RequestedById")
                        .HasColumnType("int");

                    b.Property<int>("RequestedToId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestedById");

                    b.HasIndex("RequestedToId");

                    b.ToTable("Friend");
                });

            modelBuilder.Entity("MMT_Back.EntityModels.Invitation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("InvitedUserId")
                        .HasColumnType("int");

                    b.Property<string>("StatusCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserEventId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InvitedUserId");

                    b.HasIndex("UserEventId");

                    b.ToTable("Invitation");
                });

            modelBuilder.Entity("MMT_Back.EntityModels.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Place");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "340 Chem. des Carrières, 73230 Saint-Alban-Leysse",
                            Name = "Wattabloc"
                        },
                        new
                        {
                            Id = 2,
                            Address = "95 Rue de Bolliet, 73230 Saint-Alban-Leysse",
                            Name = "Archimalt"
                        });
                });

            modelBuilder.Entity("MMT_Back.EntityModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            UserName = "Flo"
                        },
                        new
                        {
                            Id = 2,
                            UserName = "Angie"
                        });
                });

            modelBuilder.Entity("MMT_Back.EntityModels.UserEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EventPlaceId")
                        .HasColumnType("int");

                    b.Property<int>("RequesterUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventPlaceId");

                    b.HasIndex("RequesterUserId");

                    b.ToTable("UserEvents");
                });

            modelBuilder.Entity("MMT_Back.EntityModels.Friend", b =>
                {
                    b.HasOne("MMT_Back.EntityModels.User", "RequestedBy")
                        .WithMany("SentFriendRequests")
                        .HasForeignKey("RequestedById")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MMT_Back.EntityModels.User", "RequestedTo")
                        .WithMany("ReceievedFriendRequests")
                        .HasForeignKey("RequestedToId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RequestedBy");

                    b.Navigation("RequestedTo");
                });

            modelBuilder.Entity("MMT_Back.EntityModels.Invitation", b =>
                {
                    b.HasOne("MMT_Back.EntityModels.User", "InvitedUser")
                        .WithMany()
                        .HasForeignKey("InvitedUserId")
                        .OnDelete(DeleteBehavior.ClientNoAction)
                        .IsRequired();

                    b.HasOne("MMT_Back.EntityModels.UserEvent", "UserEvent")
                        .WithMany()
                        .HasForeignKey("UserEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InvitedUser");

                    b.Navigation("UserEvent");
                });

            modelBuilder.Entity("MMT_Back.EntityModels.UserEvent", b =>
                {
                    b.HasOne("MMT_Back.EntityModels.Place", "EventPlace")
                        .WithMany()
                        .HasForeignKey("EventPlaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MMT_Back.EntityModels.User", "RequesterUser")
                        .WithMany()
                        .HasForeignKey("RequesterUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventPlace");

                    b.Navigation("RequesterUser");
                });

            modelBuilder.Entity("MMT_Back.EntityModels.User", b =>
                {
                    b.Navigation("ReceievedFriendRequests");

                    b.Navigation("SentFriendRequests");
                });
#pragma warning restore 612, 618
        }
    }
}