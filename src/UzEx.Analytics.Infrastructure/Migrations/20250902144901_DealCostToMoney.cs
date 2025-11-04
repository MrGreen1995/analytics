using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UzEx.Analytics.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DealCostToMoney : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cost",
                table: "deals");

            migrationBuilder.AddColumn<decimal>(
                name: "cost_amount",
                table: "deals",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "cost_currency",
                table: "deals",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cost_amount",
                table: "deals");

            migrationBuilder.DropColumn(
                name: "cost_currency",
                table: "deals");

            migrationBuilder.AddColumn<decimal>(
                name: "cost",
                table: "deals",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
