using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourGoals.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BaseEntityDatePart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Day",
                table: "Transactions",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Month",
                table: "Transactions",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<short>(
                name: "Year",
                table: "Transactions",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<byte>(
                name: "Day",
                table: "FinancialGoals",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Month",
                table: "FinancialGoals",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<short>(
                name: "Year",
                table: "FinancialGoals",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Active",
                table: "Transactions",
                column: "Active");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialGoals_Active",
                table: "FinancialGoals",
                column: "Active");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_Active",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_FinancialGoals_Active",
                table: "FinancialGoals");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "FinancialGoals");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "FinancialGoals");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "FinancialGoals");
        }
    }
}
