using Emitix.BillingService.DTOs.Response;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Emitix.BillingService.Reports;

public class InvoiceDocument(InvoiceDto invoice) : IDocument
{
    private void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item()
                    .PaddingBottom(5)
                    .Text($"Nota Fiscal #{invoice.Number}")
                    .FontSize(24)
                    .SemiBold();

                column.Item()
                    .PaddingBottom(5)
                    .Text($"Data de Emissão: {invoice.IssuedDate.ToShortDateString()}");

                column.Item()
                    .PaddingBottom(15)
                    .Text("Remetente: Empresa Ficticia");
                
                column.Item().PaddingBottom(15)
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });
                        table.Cell().BorderBottom(1).BorderRight(1).Padding(2).Text("Endereço: Rua Ficticia, 123");
                        table.Cell().BorderBottom(1).BorderRight(1).Padding(2).Text("Cidade: Cidade Ficticia");
                        table.Cell().BorderBottom(1).Padding(2).Text("Estado: Estado Ficticio");
                    });
            });
        });
    }
    
    
    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(1, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(10));

            page.Header().Element(ComposeHeader);
            
            page.Content().Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(50);
                    columns.RelativeColumn(4);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                });
                table.Header(header =>
                {
                    header.Cell().BorderBottom(2).Padding(8).Text("#");
                    header.Cell().BorderBottom(2).Padding(8).Text("Produto");
                    header.Cell().BorderBottom(2).Padding(8).Text("Quantidade");
                    header.Cell().BorderBottom(2).Padding(8).AlignRight().Text("Preço Unitário");
                    header.Cell().BorderBottom(2).Padding(8).AlignRight().Text("Total");
                });

                var index = 1;
                foreach (var product in invoice.Products)
                {
                    table.Cell().Padding(8).Text($"{index}");
                    table.Cell().Padding(8).Text($"{product.ProductCode}");
                    table.Cell().Padding(8).Text($"{product.Quantity}");
                    table.Cell().Padding(8).AlignRight().Text($"{product.UnitPrice:C}");
                    table.Cell().Padding(8).AlignRight().Text($"{product.Subtotal:C}");
                    index++;
                }
            });
        });
    }
}