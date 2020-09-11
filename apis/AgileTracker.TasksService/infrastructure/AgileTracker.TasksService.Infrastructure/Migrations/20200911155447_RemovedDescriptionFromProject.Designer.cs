﻿// <auto-generated />
using System;
using AgileTracker.TasksService.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AgileTracker.TasksService.Infrastructure.Migrations
{
    [DbContext(typeof(AgileTrackerTasksDbContext))]
    [Migration("20200911155447_RemovedDescriptionFromProject")]
    partial class RemovedDescriptionFromProject
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AgileTracker.TasksService.Domain.Models.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProjectGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectGroupId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("AgileTracker.TasksService.Domain.Models.Entities.ProjectGroupMember", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsOwner")
                        .HasColumnType("bit");

                    b.Property<string>("MemberId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProjectGroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectGroupId");

                    b.ToTable("ProjectGroupMembers");
                });

            modelBuilder.Entity("AgileTracker.TasksService.Domain.Models.Entities.Sprint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DurationWeeks")
                        .HasColumnType("int");

                    b.Property<DateTime>("FinishedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Sprints");
                });

            modelBuilder.Entity("AgileTracker.TasksService.Domain.Models.Entities.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("FinishedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("bit");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("SprintId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("SprintId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("AgileTracker.TasksService.Domain.Models.ProjectGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProjectGroups");
                });

            modelBuilder.Entity("AgileTracker.TasksService.Domain.Models.Entities.Project", b =>
                {
                    b.HasOne("AgileTracker.TasksService.Domain.Models.ProjectGroup", null)
                        .WithMany("Projects")
                        .HasForeignKey("ProjectGroupId");
                });

            modelBuilder.Entity("AgileTracker.TasksService.Domain.Models.Entities.ProjectGroupMember", b =>
                {
                    b.HasOne("AgileTracker.TasksService.Domain.Models.ProjectGroup", null)
                        .WithMany("Members")
                        .HasForeignKey("ProjectGroupId");
                });

            modelBuilder.Entity("AgileTracker.TasksService.Domain.Models.Entities.Sprint", b =>
                {
                    b.HasOne("AgileTracker.TasksService.Domain.Models.Entities.Project", null)
                        .WithMany("Sprints")
                        .HasForeignKey("ProjectId");

                    b.OwnsMany("AgileTracker.TasksService.Domain.Models.ValueObjects.TaskStatus", "TaskStatuses", b1 =>
                        {
                            b1.Property<int>("SprintId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<bool>("IsEnd")
                                .HasColumnType("bit");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("SprintId", "Id");

                            b1.ToTable("Sprints_TaskStatuses");

                            b1.WithOwner()
                                .HasForeignKey("SprintId");
                        });
                });

            modelBuilder.Entity("AgileTracker.TasksService.Domain.Models.Entities.Task", b =>
                {
                    b.HasOne("AgileTracker.TasksService.Domain.Models.Entities.Project", null)
                        .WithMany("Backlog")
                        .HasForeignKey("ProjectId");

                    b.HasOne("AgileTracker.TasksService.Domain.Models.Entities.Sprint", null)
                        .WithMany("SprintBacklog")
                        .HasForeignKey("SprintId");

                    b.OwnsOne("AgileTracker.TasksService.Domain.Models.ValueObjects.TaskDescription", "Description", b1 =>
                        {
                            b1.Property<int>("TaskId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("AssignedToMemberId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("PointsEstimate")
                                .HasColumnType("int");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("TaskId");

                            b1.ToTable("Tasks");

                            b1.WithOwner()
                                .HasForeignKey("TaskId");
                        });

                    b.OwnsOne("AgileTracker.TasksService.Domain.Models.ValueObjects.TaskStatus", "Status", b1 =>
                        {
                            b1.Property<int>("TaskId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<bool>("IsEnd")
                                .HasColumnType("bit");

                            b1.Property<string>("Title")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("TaskId");

                            b1.ToTable("Tasks");

                            b1.WithOwner()
                                .HasForeignKey("TaskId");
                        });
                });

            modelBuilder.Entity("AgileTracker.TasksService.Domain.Models.ProjectGroup", b =>
                {
                    b.OwnsMany("AgileTracker.TasksService.Domain.Models.ValueObjects.ProjectGroupInvitation", "Invitations", b1 =>
                        {
                            b1.Property<int>("ProjectGroupId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("InvitedMemberId")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProjectGroupId", "Id");

                            b1.ToTable("ProjectGroupInvitations");

                            b1.WithOwner()
                                .HasForeignKey("ProjectGroupId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}