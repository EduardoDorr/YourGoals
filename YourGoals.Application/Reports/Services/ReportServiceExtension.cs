using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;

namespace YourGoals.Application.Reports.Services;

public static class ReportServiceExtension
{
    private static readonly XFont _baseFont = new("Verdana", 10, XFontStyle.Regular);
    private static readonly XSolidBrush _baseColor = XBrushes.Black;
    private static readonly XStringFormat _baseFormat =
        new XStringFormat()
        {
            LineAlignment = XLineAlignment.Near,
            Alignment = XStringAlignment.Near
        };

    private static readonly XPen _basePen = XPens.Black;

    public static void DrawContent(this XGraphics graphics,
                                   string content,
                                   int width = 0,
                                   int height = 0,
                                   XFont? font = null,
                                   XSolidBrush? color = null,
                                   XStringFormat? format = null)
    {
        if (content is null)
            return;

        font ??= _baseFont;
        color ??= _baseColor;
        format ??= _baseFormat;

        graphics.DrawString(
            content,
            font,
            color,
            new XRect(width, height, graphics.PdfPage.Width, graphics.PdfPage.Height),
            format);
    }

    public static void DrawSimpleLine(this XGraphics graphics,
                                      int width = 20,
                                      int height = 0,
                                      XPen? pen = null)
    {
        pen ??= _basePen;

        graphics.DrawLine(
            pen,
            width,
            height,
            graphics.PdfPage.Width - 20,
            height);
    }
}