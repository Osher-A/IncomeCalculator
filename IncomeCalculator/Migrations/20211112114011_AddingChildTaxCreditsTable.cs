using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace IncomeCalculator.Migrations
{
    public partial class AddingChildTaxCreditsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChildTaxCredits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FamilyElement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ChildElement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Threshold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WithdrawalRate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildTaxCredits", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildTaxCredits");
        }
    }
}
