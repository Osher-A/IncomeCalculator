using Microsoft.EntityFrameworkCore.Migrations;

namespace IncomeCalculator.Migrations
{
    public partial class ChangingYearColumnToTaxYearAndTotalTypeToDecimalInMinWageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "MinWages",
                newName: "TaxYear");

            migrationBuilder.AlterColumn<decimal>(
                name: "Wage",
                table: "MinWages",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxYear",
                table: "MinWages",
                newName: "Year");

            migrationBuilder.AlterColumn<float>(
                name: "Wage",
                table: "MinWages",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
