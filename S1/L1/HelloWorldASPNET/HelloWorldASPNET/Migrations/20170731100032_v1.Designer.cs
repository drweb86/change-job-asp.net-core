using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HelloWorldASPNET.Services;

namespace HelloWorldASPNET.Migrations
{
    [DbContext(typeof(LBartoContext))]
    [Migration("20170731100032_v1")]
    partial class v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HelloWorldASPNET.Entities.Person2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Age");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });
        }
    }
}
