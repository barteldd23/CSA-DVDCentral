using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Paragraph = iText.Layout.Element.Paragraph;
using Table = iText.Layout.Element.Table;

namespace DDB.Reporting
{
    public static class Excel
    {
        public static void Export(string filename, string[,] data)
        {
			try
			{
				IXLWorkbook xlWorkbook = new XLWorkbook();
				IXLWorksheet xlWorksheet = xlWorkbook.AddWorksheet("Data");

				int rows = data.GetLength(0);
				int columns = data.GetLength(1);

				// -4 for the xlsx
				PdfWriter writer = new PdfWriter(filename.Substring(0,filename.Length-4) + "pdf");
				PdfDocument pdf = new PdfDocument(writer);
				Document document = new Document(pdf);

				Paragraph header = new Paragraph("Data")
					.SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
					.SetFontSize(20);
				document.Add(header);

                Paragraph subHeader = new Paragraph("Information")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(15);
                document.Add(subHeader);

				Table table = new Table(columns, false);


				// excel cels start at 1,1 
				for (int iRow = 1; iRow <= rows; iRow++)
				{
					for (int iCol = 1; iCol < columns; iCol++)
					{
						// Excel
						// The data Array is 0 based.
						xlWorksheet.Cell(iRow,iCol).Value = data[iRow - 1,iCol - 1];

						// PDF
						Cell cell = new Cell(1,1);
						cell.Add(new Paragraph(data[iRow - 1,iCol - 1]));
						table.AddCell(cell);
					}
				}


				document.Add(table);

				document.Close();
				xlWorkbook.SaveAs(filename);

            }
			catch (Exception)
			{

				throw;
			}
        }
    }
}
