using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FribergHomes.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Presentation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Realtors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Realtors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Realtors_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rooms = table.Column<int>(type: "int", nullable: false),
                    LivingArea = table.Column<double>(type: "float", nullable: false),
                    AncillaryArea = table.Column<double>(type: "float", nullable: false),
                    PlotArea = table.Column<double>(type: "float", nullable: true),
                    YearlyCost = table.Column<double>(type: "float", nullable: true),
                    MonthlyFee = table.Column<double>(type: "float", nullable: true),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Lift = table.Column<bool>(type: "bit", nullable: true),
                    ListingPrice = table.Column<int>(type: "int", nullable: false),
                    CurrentPrice = table.Column<int>(type: "int", nullable: false),
                    ObjectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuildYear = table.Column<int>(type: "int", nullable: false),
                    ImageLinks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViewingDates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RealtorId = table.Column<int>(type: "int", nullable: true),
                    CountyId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesObjects_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesObjects_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SalesObjects_Realtors_RealtorId",
                        column: x => x.RealtorId,
                        principalTable: "Realtors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Realtors_AgencyId",
                table: "Realtors",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesObjects_CategoryId",
                table: "SalesObjects",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesObjects_CountyId",
                table: "SalesObjects",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesObjects_RealtorId",
                table: "SalesObjects",
                column: "RealtorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesObjects");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropTable(
                name: "Realtors");

            migrationBuilder.DropTable(
                name: "Agencies");
        }
    }
}
