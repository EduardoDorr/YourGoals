using Microsoft.Extensions.Hosting;

using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;

using YourGoals.Core.Results;
using YourGoals.Domain.Transactions.Enums;
using YourGoals.Domain.Transactions.Entities;
using YourGoals.Domain.FinancialGoals.Entities;

namespace YourGoals.Application.Reports.Services;

public sealed class ReportService : IReportService
{
    private readonly string _staticFilesPath;

    private readonly XFont _titleFont;
    private readonly XFont _subTitleFont;
    private readonly XFont _contentFont;

    public ReportService(IHostEnvironment environment)
    {
        _staticFilesPath = Path.Combine(environment.ContentRootPath, "wwwroot");

        _titleFont = new("Verdana", 20, XFontStyle.Bold);
        _subTitleFont = new("Verdana", 12, XFontStyle.Bold);
        _contentFont = new("Verdana", 10, XFontStyle.Regular);
    }

    public Result<string> GenerateFinancialGoalReport(FinancialGoal financialGoal)
    {
        var transactions = financialGoal.Transactions;

        var document = new PdfDocument();
        var page = document.AddPage();
        var graphics = XGraphics.FromPdfPage(page);

        var height = 5;

        height = GenerateHeader(financialGoal, graphics, height);

        height = GenerateTransactions(transactions, graphics, height);

        var financialGoalReportsDirectory = Path.Combine(_staticFilesPath, "reports", "financialGoalTransactions");
        var financialGoalReportFullPath = Path.Combine(financialGoalReportsDirectory, $"{financialGoal.Name}-Report.pdf");

        if (!Directory.Exists(financialGoalReportsDirectory))
            Directory.CreateDirectory(financialGoalReportsDirectory);

        document.Save(financialGoalReportFullPath);

        return Result.Ok(financialGoalReportFullPath);
    }

    private int GenerateTransactions(IReadOnlyCollection<Transaction> transactions, XGraphics graphics, int height)
    {
        var transactionTypes = new List<TransactionType>();
        var page = graphics.PdfPage;

        foreach (var transaction in transactions.OrderBy(t => t.Type))
        {
            if (!transactionTypes.Contains(transaction.Type))
            {
                height += 10;

                switch (transaction.Type)
                {
                    case TransactionType.Deposit:
                        height = GenerateByType("Depósito", transactions.Where(x => x.Type == TransactionType.Deposit).ToList(), graphics, height);
                        transactionTypes.Add(TransactionType.Deposit);
                        break;
                    case TransactionType.Withdraw:
                        height = GenerateByType("Saque", transactions.Where(x => x.Type == TransactionType.Withdraw).ToList(), graphics, height);
                        transactionTypes.Add(TransactionType.Withdraw);
                        break;
                    case TransactionType.Interest:
                        height = GenerateByType("JAM", transactions.Where(x => x.Type == TransactionType.Interest).ToList(), graphics, height);
                        transactionTypes.Add(TransactionType.Interest);
                        break;
                    default:
                        break;
                }
            }
        }

        return height;
    }

    private int GenerateHeader(FinancialGoal financialGoal, XGraphics graphics, int height)
    {
        var titleFormat =
        new XStringFormat()
        {
            LineAlignment = XLineAlignment.Near,
            Alignment = XStringAlignment.Center
        };

        graphics.DrawContent("RELATÓRIO", width: 20, height: height, font: _titleFont, format: titleFormat);

        var leftSpaceColumn1 = 20;
        var leftSpaceColumn2 = 300;
        height += 30;

        graphics.DrawSimpleLine(height: height);

        height += 5;

        graphics.DrawContent($"Meta Financeira: {financialGoal.Name}", width: leftSpaceColumn1, height: height, font: _subTitleFont);
        graphics.DrawContent($"Iniciada em: {financialGoal.CreatedAt}", width: leftSpaceColumn2, height: height, font: _subTitleFont);

        height += 20;

        graphics.DrawContent($"Meta: R$ {financialGoal.GoalAmount}", width: leftSpaceColumn1, height: height, font: _subTitleFont);
        graphics.DrawContent($"Status: {financialGoal.Status}", width: leftSpaceColumn2, height: height, font: _subTitleFont);

        height += 20;

        var deadline = financialGoal.Deadline.HasValue ? $"Prazo: {financialGoal.Deadline:dd/MM/yyyy}" : "";
        var interest = financialGoal.InterestRate.HasValue ? $"Taxa de Juros: {financialGoal.InterestRate * 100.0m} %" : "";

        if (!string.IsNullOrWhiteSpace(deadline))
        {
            graphics.DrawContent($"{deadline}", width: leftSpaceColumn1, height: height, font: _subTitleFont);

            if (!string.IsNullOrWhiteSpace(interest))
                graphics.DrawContent($"{interest}", width: leftSpaceColumn2, height: height, font: _subTitleFont);
        }
        else if (!string.IsNullOrWhiteSpace(interest))
            graphics.DrawContent($"{interest}", width: leftSpaceColumn1, height: height, font: _subTitleFont);

        height += 20;

        graphics.DrawSimpleLine(height: height);

        return height;
    }

    private int GenerateByType(string transactionType, List<Transaction> transactions, XGraphics graphics, int height)
    {
        graphics.DrawContent($"{transactionType.ToUpper()}", width: 20, height: height, font: _subTitleFont);

        foreach (var transaction in transactions.OrderBy(t => t.TransactionDate))
        {
            height += 18;
            GenerateTransaction(transaction, graphics, height);
        }

        height += 20;

        graphics.DrawContent($"Total de Transações: {transactions.Count}", width: 20, height: height, font: _subTitleFont);

        height += 20;

        graphics.DrawSimpleLine(height: height);

        return height;
    }

    private void GenerateTransaction(Transaction transaction, XGraphics graphics, int height)
    {
        graphics.DrawContent($"Valor: {transaction.Amount}", width: 20, height: height, font: _contentFont);

        graphics.DrawContent($"Data da Transação: {transaction.TransactionDate:dd-MM-yyyy}", width: 200, height: height, font: _contentFont);

        graphics.DrawContent($"Estornada: {(transaction.Active ? "Não" : "Sim")}", width: 400, height: height, font: _contentFont);
    }
}