﻿// <auto-generated />
using FluentHttpClient.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FluentHttpClient.Entity.Migrations
{
    [DbContext(typeof(FluentHttpClientContext))]
    [Migration("20190306190736_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FluentlyHttpClient.Caching.MessageItemStore", b =>
                {
                    b.Property<string>("Hash")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(70);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("ContentHeaders")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("Headers")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("ReasonPhrase")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("RequestMessage")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<int>("StatusCode");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasMaxLength(30);

                    b.HasKey("Hash");

                    b.ToTable("HttpResponse","cache");
                });
#pragma warning restore 612, 618
        }
    }
}
