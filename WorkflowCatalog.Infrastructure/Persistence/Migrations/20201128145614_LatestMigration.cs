using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkflowCatalog.Infrastructure.Persistence.Migrations
{
    public partial class LatestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Actors_UseCases_UseCaseId",
                table: "Actors");

            migrationBuilder.DropForeignKey(
                name: "FK_UseCases_Actors_UseCaseActorId",
                table: "UseCases");

            migrationBuilder.DropIndex(
                name: "IX_UseCases_UseCaseActorId",
                table: "UseCases");

            migrationBuilder.DropIndex(
                name: "IX_Actors_UseCaseId",
                table: "Actors");

            migrationBuilder.DropColumn(
                name: "UseCaseActorId",
                table: "UseCases");

            migrationBuilder.DropColumn(
                name: "UseCaseId",
                table: "Actors");

            migrationBuilder.CreateTable(
                name: "UseCaseActor",
                columns: table => new
                {
                    UseCaseId = table.Column<Guid>(nullable: false),
                    ActorId = table.Column<Guid>(nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UseCaseActor", x => new { x.UseCaseId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_UseCaseActor_Actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UseCaseActor_UseCases_UseCaseId",
                        column: x => x.UseCaseId,
                        principalTable: "UseCases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UseCaseActor_ActorId",
                table: "UseCaseActor",
                column: "ActorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UseCaseActor");

            migrationBuilder.AddColumn<Guid>(
                name: "UseCaseActorId",
                table: "UseCases",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UseCaseId",
                table: "Actors",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UseCases_UseCaseActorId",
                table: "UseCases",
                column: "UseCaseActorId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_UseCaseId",
                table: "Actors",
                column: "UseCaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Actors_UseCases_UseCaseId",
                table: "Actors",
                column: "UseCaseId",
                principalTable: "UseCases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UseCases_Actors_UseCaseActorId",
                table: "UseCases",
                column: "UseCaseActorId",
                principalTable: "Actors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
