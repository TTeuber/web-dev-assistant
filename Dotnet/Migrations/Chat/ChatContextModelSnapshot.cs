﻿// <auto-generated />
using System;
using Dotnet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Dotnet.Migrations.Chat
{
    [DbContext(typeof(ChatContext))]
    partial class ChatContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("Dotnet.Models.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Chat");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ed49ef44-a87e-4c90-a4d1-223910a05d9b"),
                            Title = "New Chat"
                        });
                });

            modelBuilder.Entity("Dotnet.Models.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.ToTable("Message");

                    b.HasData(
                        new
                        {
                            Id = new Guid("97e3edd3-0a58-47d3-9084-34fb8f7e5aee"),
                            ChatId = new Guid("e80c149f-58db-4746-9c29-2ec268266462"),
                            Content = "Hello World!",
                            Role = "user"
                        },
                        new
                        {
                            Id = new Guid("1e074283-1eda-443b-b304-e74de7f0b162"),
                            ChatId = new Guid("e3a9cfac-fad4-448a-9a2b-a6374728a74d"),
                            Content = "Hello Person!",
                            Role = "assistant"
                        },
                        new
                        {
                            Id = new Guid("7e5f5e01-616b-4359-b3da-a27987ac4d19"),
                            ChatId = new Guid("c91933ae-4a37-42e2-886a-244117b3ee7d"),
                            Content = "Goodbye!",
                            Role = "user"
                        });
                });

            modelBuilder.Entity("Dotnet.Models.Message", b =>
                {
                    b.HasOne("Dotnet.Models.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");
                });

            modelBuilder.Entity("Dotnet.Models.Chat", b =>
                {
                    b.Navigation("Messages");
                });
#pragma warning restore 612, 618
        }
    }
}