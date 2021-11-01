using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IncomeCalculator.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "MinWages",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Age = table.Column<int>(type: "int", nullable: false),
            //        Year = table.Column<DateTime>(type: "date", nullable: false),
            //        Wage = table.Column<float>(type: "real", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MinWages", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MinWages");
        }
    }
}
