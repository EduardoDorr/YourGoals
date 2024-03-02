using Microsoft.Extensions.Hosting;

using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;

using YourGoals.Core.Results;
using YourGoals.Domain.Transactions.Enums;
using YourGoals.Domain.Transactions.Entities;
using YourGoals.Domain.FinancialGoals.Entities;

namespace YourGoals.Application.Reports.Service;

public sealed class ReportService : IReportService
{
    private readonly string _staticFilesPath;

    private readonly XFont _titleFont = new("Verdana", 20, XFontStyle.Bold);
    private readonly XFont _subTitleFont = new("Verdana", 12, XFontStyle.Bold);
    private readonly XFont _subTextFont = new("Verdana", 10, XFontStyle.Regular);

    public ReportService(IHostEnvironment environment)
    {
        _staticFilesPath = Path.Combine(environment.ContentRootPath, "wwwroot");
    }

    public Result<string> GenerateFinancialGoalReport(FinancialGoal financialGoal)
    {
        var transactions = financialGoal.Transactions;

        var document = new PdfDocument();
        var page = document.AddPage();
        var graphics = XGraphics.FromPdfPage(page);

        var height = 5;

        height = GenerateHeader(financialGoal, page, graphics, height);

        var transactionTypes = new List<TransactionType>();

        foreach (var transaction in transactions.OrderBy(t => t.Type))
        {
            if (!transactionTypes.Contains(transaction.Type))
            {
                height += 10;

                switch (transaction.Type)
                {
                    case TransactionType.Deposit:
                        height = GenerateByType("Depósito", transactions.Where(x => x.Type == TransactionType.Deposit).ToList(), graphics, page, height);
                        transactionTypes.Add(TransactionType.Deposit);
                        break;
                    case TransactionType.Withdraw:
                        height = GenerateByType("Saque", transactions.Where(x => x.Type == TransactionType.Withdraw).ToList(), graphics, page, height);
                        transactionTypes.Add(TransactionType.Withdraw);
                        break;
                    case TransactionType.Interest:
                        height = GenerateByType("JAM", transactions.Where(x => x.Type == TransactionType.Interest).ToList(), graphics, page, height);
                        transactionTypes.Add(TransactionType.Interest);
                        break;
                    default:
                        break;
                }
            }
        }

        var financialGoalReportsDirectory = Path.Combine(_staticFilesPath, "reports", "financialGoalTransactions");
        var financialGoalReportFullPath = Path.Combine(financialGoalReportsDirectory, $"{financialGoal.Name}-Report.pdf");

        if (!Directory.Exists(financialGoalReportsDirectory))
            Directory.CreateDirectory(financialGoalReportsDirectory);

        document.Save(financialGoalReportFullPath);

        return Result.Ok(financialGoalReportFullPath);
    }

    private int GenerateHeader(FinancialGoal financialGoal, PdfPage page, XGraphics graphics, int height)
    {
        graphics.DrawString
                    (
                        "RELATÓRIO",
                        _titleFont,
                        XBrushes.Black,
                        new XRect(0, height, page.Width, page.Height),
                        new XStringFormat()
                        {
                            LineAlignment = XLineAlignment.Near,
                            Alignment = XStringAlignment.Center
                        }
                    );

        var leftSpaceColumn1 = 20;
        var leftSpaceColumn2 = 300;
        height += 30;

        graphics.DrawLine(XPens.Black, 20, height, page.Width - 20, height);
        
        height += 5;

        graphics.DrawString
            (
                $"Meta Financeira: {financialGoal.Name}",
                _subTitleFont,
                XBrushes.Black,
                new XRect(leftSpaceColumn1, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                }
            );

        graphics.DrawString
            (
                $"Iniciada em: {financialGoal.CreatedAt}",
                _subTitleFont,
                XBrushes.Black,
                new XRect(leftSpaceColumn2, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                }
            );

        height += 20;

        graphics.DrawString
            (
                $"Meta: R$ {financialGoal.GoalAmount}",
                _subTitleFont,
                XBrushes.Black,
                new XRect(leftSpaceColumn1, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                }
            );

        graphics.DrawString
            (
                $"Status: {financialGoal.Status}",
                _subTitleFont,
                XBrushes.Black,
                new XRect(leftSpaceColumn2, height, page.Width, page.Height),
                new XStringFormat()
                {
                    LineAlignment = XLineAlignment.Near,
                    Alignment = XStringAlignment.Near
                }
            );

        height += 20;

        var deadline = financialGoal.Deadline.HasValue ? $"Prazo: {financialGoal.Deadline:dd/MM/yyyy}" : "";
        var interest = financialGoal.InterestRate.HasValue ? $"Taxa de Juros: {financialGoal.InterestRate * 100.0m} %" : "";

        if (!string.IsNullOrWhiteSpace(deadline))
        {
            graphics.DrawString
                (
                    $"{deadline}",
                    _subTitleFont,
                    XBrushes.Black,
                    new XRect(leftSpaceColumn1, height, page.Width, page.Height),
                    new XStringFormat()
                    {
                        LineAlignment = XLineAlignment.Near,
                        Alignment = XStringAlignment.Near
                    }
                );

            if (!string.IsNullOrWhiteSpace(interest))
            {
                graphics.DrawString
                    (
                        $"{interest}",
                        _subTitleFont,
                        XBrushes.Black,
                        new XRect(leftSpaceColumn2, height, page.Width, page.Height),
                        new XStringFormat()
                        {
                            LineAlignment = XLineAlignment.Near,
                            Alignment = XStringAlignment.Near
                        }
                    );
            }
        }        
        else if (!string.IsNullOrWhiteSpace(interest))
        {
            graphics.DrawString
                (
                    $"{interest}",
                    _subTitleFont,
                    XBrushes.Black,
                    new XRect(leftSpaceColumn1, height, page.Width, page.Height),
                    new XStringFormat()
                    {
                        LineAlignment = XLineAlignment.Near,
                        Alignment = XStringAlignment.Near
                    }
                );
        }

        height += 20;

        graphics.DrawLine(XPens.Black, 20, height, page.Width - 20, height);

        return height;
    }

    private int GenerateByType(string transactionType, List<Transaction> transactions, XGraphics graphics, PdfPage page, int height)
    {
        graphics.DrawString(
            $"{transactionType.ToUpper()}",
            _subTitleFont,
            XBrushes.Black,
            new XRect(20, height, page.Width, page.Height),
            new XStringFormat()
            {
                LineAlignment = XLineAlignment.Near,
                Alignment = XStringAlignment.Near
            });

        foreach (var transaction in transactions.OrderBy(t => t.TransactionDate))
        {
            height += 18;
            GenerateTransaction(transaction, graphics, page, height);
        }

        height += 20;
        graphics.DrawString(
        $"Total de Transações: {transactions.Count}",
              _subTitleFont,
              XBrushes.Black,
              new XRect(20, height, page.Width, page.Height),
              new XStringFormat()
              {
                  LineAlignment = XLineAlignment.Near,
                  Alignment = XStringAlignment.Near
              });

        height += 20;

        graphics.DrawLine(XPens.Black, 20, height, page.Width - 20, height);

        return height;
    }

    private void GenerateTransaction(Transaction transaction, XGraphics graphics, PdfPage page, int height)
    {
        graphics.DrawString(
            $"Valor: {transaction.Amount}",
            _subTextFont,
            XBrushes.Black,
            new XRect(20, height, page.Width, page.Height),
            new XStringFormat()
            {
                LineAlignment = XLineAlignment.Near,
                Alignment = XStringAlignment.Near
            });

        graphics.DrawString(
            $"Data da Transação: {transaction.TransactionDate:dd-MM-yyyy}",
            _subTextFont,
            XBrushes.Black,
            new XRect(200, height, page.Width, page.Height),
            new XStringFormat()
            {
                LineAlignment = XLineAlignment.Near,
                Alignment = XStringAlignment.Near
            });

        graphics.DrawString(
            $"Estornada: {(transaction.Active ? "Não" : "Sim")}",
            _subTextFont,
            XBrushes.Black,
            new XRect(400, height, page.Width, page.Height),
            new XStringFormat()
            {
                LineAlignment = XLineAlignment.Near,
                Alignment = XStringAlignment.Near
            });
    }
}