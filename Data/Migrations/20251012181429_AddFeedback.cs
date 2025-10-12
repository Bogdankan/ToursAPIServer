using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToursAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourVisit_AspNetUsers_UserId",
                table: "TourVisit");

            migrationBuilder.DropForeignKey(
                name: "FK_TourVisit_Tours_TourId",
                table: "TourVisit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourVisit",
                table: "TourVisit");

            migrationBuilder.RenameTable(
                name: "TourVisit",
                newName: "TourVisits");

            migrationBuilder.RenameIndex(
                name: "IX_TourVisit_UserId",
                table: "TourVisits",
                newName: "IX_TourVisits_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TourVisit_TourId",
                table: "TourVisits",
                newName: "IX_TourVisits_TourId");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "TourVisits",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourVisits",
                table: "TourVisits",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TourId = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_TourId",
                table: "Feedbacks",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourVisits_AspNetUsers_UserId",
                table: "TourVisits",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourVisits_Tours_TourId",
                table: "TourVisits",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourVisits_AspNetUsers_UserId",
                table: "TourVisits");

            migrationBuilder.DropForeignKey(
                name: "FK_TourVisits_Tours_TourId",
                table: "TourVisits");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TourVisits",
                table: "TourVisits");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "TourVisits");

            migrationBuilder.RenameTable(
                name: "TourVisits",
                newName: "TourVisit");

            migrationBuilder.RenameIndex(
                name: "IX_TourVisits_UserId",
                table: "TourVisit",
                newName: "IX_TourVisit_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TourVisits_TourId",
                table: "TourVisit",
                newName: "IX_TourVisit_TourId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TourVisit",
                table: "TourVisit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourVisit_AspNetUsers_UserId",
                table: "TourVisit",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourVisit_Tours_TourId",
                table: "TourVisit",
                column: "TourId",
                principalTable: "Tours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
