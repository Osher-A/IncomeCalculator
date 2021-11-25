using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace IncomeCalculator.Migrations
{
    public partial class AddingWorkingTaxCreditsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkingTaxCredits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BasicElement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SecondAdult = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThirtyHourElement = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Threshold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WithdrawalRate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingTaxCredits", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingTaxCredits");
        }
    }
}
