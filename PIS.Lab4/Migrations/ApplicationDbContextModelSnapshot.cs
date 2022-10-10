﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PIS.Lab4.DataAccess;

namespace PIS.Lab4.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JobWorker", b =>
                {
                    b.Property<int>("JobsJobID")
                        .HasColumnType("int");

                    b.Property<int>("WorkersWorkerID")
                        .HasColumnType("int");

                    b.HasKey("JobsJobID", "WorkersWorkerID");

                    b.HasIndex("WorkersWorkerID");

                    b.ToTable("JobWorker");
                });

            modelBuilder.Entity("PIS.Lab4.Models.Job", b =>
                {
                    b.Property<int>("JobID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.HasKey("JobID");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("PIS.Lab4.Models.Worker", b =>
                {
                    b.Property<int>("WorkerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkplaceID")
                        .HasColumnType("int");

                    b.HasKey("WorkerID");

                    b.HasIndex("WorkplaceID");

                    b.ToTable("Worker");
                });

            modelBuilder.Entity("PIS.Lab4.Models.Workplace", b =>
                {
                    b.Property<int>("WorkplaceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LongName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WorkplaceID");

                    b.ToTable("Workplace");
                });

            modelBuilder.Entity("JobWorker", b =>
                {
                    b.HasOne("PIS.Lab4.Models.Job", null)
                        .WithMany()
                        .HasForeignKey("JobsJobID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PIS.Lab4.Models.Worker", null)
                        .WithMany()
                        .HasForeignKey("WorkersWorkerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PIS.Lab4.Models.Worker", b =>
                {
                    b.HasOne("PIS.Lab4.Models.Workplace", "Workplace")
                        .WithMany("Workers")
                        .HasForeignKey("WorkplaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workplace");
                });

            modelBuilder.Entity("PIS.Lab4.Models.Workplace", b =>
                {
                    b.Navigation("Workers");
                });
#pragma warning restore 612, 618
        }
    }
}
