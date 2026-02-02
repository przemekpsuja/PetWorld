using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PetWorld.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ChatHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Question = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Answer = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IterationCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatHistory", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Karma dla psów", "Premium karma dla dorosłych psów średnich ras", "Royal Canin Adult Dog 15kg", 289m },
                    { 2, "Karma dla kotów", "Sucha karma dla dorosłych kotów z kurczakiem", "Whiskas Adult Kurczak 7kg", 129m },
                    { 3, "Akwarystyka", "Uzdatniacz wody do akwarium, neutralizuje chlor", "Tetra AquaSafe 500ml", 45m },
                    { 4, "Akcesoria dla kotów", "Wysoki drapak z platformami i domkiem", "Trixie Drapak XL 150cm", 399m },
                    { 5, "Zabawki dla psów", "Wytrzymała zabawka do napełniania smakołykami", "Kong Classic Large", 69m },
                    { 6, "Gryzonie", "Klatka 60x40cm z wyposażeniem", "Ferplast Klatka dla gryzonia", 189m },
                    { 7, "Akcesoria dla psów", "Smycz zwijana dla psów do 50kg", "Flexi Smycz automatyczna 8m", 119m },
                    { 8, "Karma dla kotów", "Karma dla kociąt do 12 miesiąca życia", "Brit Premium Kitten 8kg", 159m },
                    { 9, "Akwarystyka", "Kompletny zestaw CO2 dla roślin akwariowych", "JBL ProFlora CO2 Set", 549m },
                    { 10, "Gryzonie", "Naturalne siano łąkowe, podstawa diety", "Vitapol Siano dla królików 1kg", 25m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatHistory");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
