using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourGoals.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCommit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialGoals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GoalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    CurrentAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    InitialAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    InterestRate = table.Column<decimal>(type: "numeric(5,2)", nullable: true),
                    Deadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdealMonthlySaving = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialGoals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FinancialGoalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_FinancialGoals_FinancialGoalId",
                        column: x => x.FinancialGoalId,
                        principalTable: "FinancialGoals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialGoals_Name",
                table: "FinancialGoals",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_FinancialGoalId",
                table: "Transactions",
                column: "FinancialGoalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "FinancialGoals");
        }
    }
}
