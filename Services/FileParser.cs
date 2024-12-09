using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using System.Reflection.PortableExecutable;

namespace ResumeAnalyzer.Services
{
    public static class FileParser
    {
        public static string ExtractText(string filePath)
        {
            using (PdfReader reader = new PdfReader(filePath))
            using (PdfDocument pdfDoc = new PdfDocument(reader))
            {
                var text = new System.Text.StringBuilder();
                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i)));
                }
                return text.ToString();
            }
        }
    }
}
