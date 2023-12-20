﻿// <auto-generated />
using System;
using DataBases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataBases.Migrations
{
    [DbContext(typeof(ChatContext))]
    [Migration("20231220121855_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataBases.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<bool>("IsSent")
                        .HasColumnType("bit")
                        .HasColumnName("is_sent");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("message_text");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2")
                        .HasColumnName("message_date");

                    b.Property<int?>("UserFromId")
                        .HasColumnType("int");

                    b.Property<int?>("UserToId")
                        .HasColumnType("int");

                    b.HasKey("MessageId")
                        .HasName("messages_pkey");

                    b.HasIndex("UserFromId");

                    b.HasIndex("UserToId");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("DataBases.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("FullName");

                    b.HasKey("Id")
                        .HasName("users_pkey");

                    b.HasIndex("Fullname")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("DataBases.Message", b =>
                {
                    b.HasOne("DataBases.User", "SenderId")
                        .WithMany("MessagesFrom")
                        .HasForeignKey("UserFromId")
                        .HasConstraintName("message_from_user_foreign_key");

                    b.HasOne("DataBases.User", "RecieverId")
                        .WithMany("MessagesTo")
                        .HasForeignKey("UserToId")
                        .HasConstraintName("message_to_user_foreign_key");

                    b.Navigation("RecieverId");

                    b.Navigation("SenderId");
                });

            modelBuilder.Entity("DataBases.User", b =>
                {
                    b.Navigation("MessagesFrom");

                    b.Navigation("MessagesTo");
                });
#pragma warning restore 612, 618
        }
    }
}
