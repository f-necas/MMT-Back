﻿// <auto-generated />
using System;
using MMT_Back;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

#nullable disable

namespace MMT_Back.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20211031221131_addCoordinatesToPlace")]
    partial class addCoordinatesToPlace
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0-rc.2.21480.5")
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

                    b.HasAlternateKey("RequestedById", "RequestedToId")
                        .HasName("FriendUniqueAltKey");

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

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InvitedUserId");

                    b.HasIndex("UserEventId");

                    b.HasIndex("UserId");

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

                    b.Property<Point>("Coordinate")
                        .HasColumnType("geography");

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
                            Coordinate = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (45.57528871439694 5.9587356460298855)"),
                            Name = "Wattabloc"
                        },
                        new
                        {
                            Id = 2,
                            Address = "95 Rue de Bolliet, 73230 Saint-Alban-Leysse",
                            Coordinate = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (45.57510642776163 5.949668211603892)"),
                            Name = "Archimalt"
                        });
                });

            modelBuilder.Entity("MMT_Back.EntityModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "flo",
                            UserName = "Flo"
                        },
                        new
                        {
                            Id = 2,
                            Password = "angie",
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

                    b.HasOne("MMT_Back.EntityModels.User", null)
                        .WithMany("Invitations")
                        .HasForeignKey("UserId");

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
                        .WithMany("UserEvents")
                        .HasForeignKey("RequesterUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EventPlace");

                    b.Navigation("RequesterUser");
                });

            modelBuilder.Entity("MMT_Back.EntityModels.User", b =>
                {
                    b.Navigation("Invitations");

                    b.Navigation("ReceievedFriendRequests");

                    b.Navigation("SentFriendRequests");

                    b.Navigation("UserEvents");
                });
#pragma warning restore 612, 618
        }
    }
}