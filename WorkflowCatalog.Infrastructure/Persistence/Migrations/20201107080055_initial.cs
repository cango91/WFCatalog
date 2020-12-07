﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkflowCatalog.Infrastructure.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    ShortName = table.Column<string>(maxLength: 6, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UseCases",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Preconditions = table.Column<string>(nullable: true),
                    Postconditions = table.Column<string>(nullable: true),
                    NormalCourse = table.Column<string>(nullable: true),
                    AltCourse = table.Column<string>(nullable: true),
                    WorkflowId = table.Column<Guid>(nullable: false),
                    UseCaseActorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseCases", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SetupId = table.Column<Guid>(nullable: false),
                    UseCaseId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actors_Setups_SetupId",
                        column: x => x.SetupId,
                        principalTable: "Setups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Actors_UseCases_UseCaseId",
                        column: x => x.UseCaseId,
                        principalTable: "UseCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Workflows",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    SetupId = table.Column<Guid>(nullable: false),
                    PrimaryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workflows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workflows_Setups_SetupId",
                        column: x => x.SetupId,
                        principalTable: "Setups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diagrams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Name_Name = table.Column<string>(nullable: true),
                    Name_Extension = table.Column<string>(nullable: true),
                    ContentType = table.Column<string>(nullable: false),
                    WorkflowId = table.Column<Guid>(nullable: false),
                    File = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagrams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagrams_Workflows_WorkflowId",
                        column: x => x.WorkflowId,
                        principalTable: "Workflows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_SetupId",
                table: "Actors",
                column: "SetupId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_UseCaseId",
                table: "Actors",
                column: "UseCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagrams_WorkflowId",
                table: "Diagrams",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_UseCases_UseCaseActorId",
                table: "UseCases",
                column: "UseCaseActorId");

            migrationBuilder.CreateIndex(
                name: "IX_UseCases_WorkflowId",
                table: "UseCases",
                column: "WorkflowId");

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_PrimaryId",
                table: "Workflows",
                column: "PrimaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Workflows_SetupId",
                table: "Workflows",
                column: "SetupId");

            migrationBuilder.AddForeignKey(
                name: "FK_UseCases_Workflows_WorkflowId",
                table: "UseCases",
                column: "WorkflowId",
                principalTable: "Workflows",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UseCases_Actors_UseCaseActorId",
                table: "UseCases",
                column: "UseCaseActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workflows_Diagrams_PrimaryId",
                table: "Workflows",
                column: "PrimaryId",
                principalTable: "Diagrams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_Setups_SetupId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Workflows_Setups_SetupId",
                table: "Workflows");

            migrationBuilder.DropForeignKey(
                name: "FK_Actors_UseCases_UseCaseId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_Diagrams_Workflows_WorkflowId",
                table: "Diagrams");

            migrationBuilder.DropTable(
                name: "Setups");

            migrationBuilder.DropTable(
                name: "UseCases");

            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Workflows");

            migrationBuilder.DropTable(
                name: "Diagrams");
        }
    }
}