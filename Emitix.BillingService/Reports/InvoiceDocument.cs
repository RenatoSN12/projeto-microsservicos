using Emitix.BillingService.DTOs.Response;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ZXing;
using ZXing.OneD;
using ZXing.Rendering;
using IContainer = QuestPDF.Infrastructure.IContainer;

namespace Emitix.BillingService.Reports;

public class InvoiceDocument(InvoiceDto invoice) : IDocument
{
    private void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Row(rowDescriptor =>
                {
                    rowDescriptor.RelativeItem().PaddingBottom(5)
                        .Text($"Nota Fiscal #{invoice.Number}")
                        .FontSize(24)
                        .SemiBold();
                    
                    rowDescriptor.RelativeItem().Width(250).Height(60).Svg(size =>
                    {
                        var writer = new Code128Writer();
                        var eanCode = writer.encode(invoice.Id.ToString(), BarcodeFormat.CODE_128, (int) size.Width, (int) size.Height);
                        var renderer = new SvgRenderer { FontName = "Lato", FontSize = 16 };
                        return renderer.Render(eanCode, BarcodeFormat.CODE_128, invoice.Id.ToString()).Content;
                    });
                    
                });
                
                column.Item()
                    .PaddingBottom(5)
                    .Text($"Data de Emissão: {invoice.IssuedDate.ToShortDateString()}");

                column.Item()
                    .PaddingBottom(5)
                    .Text("Remetente: Empresa Ficticia");
                
                column.Item()
                    .PaddingBottom(15)
                    .Text("Inscrição Estadual: Isento");
                
                column.Item().PaddingBottom(70)
                    .Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn(2);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });
                        table.Cell().BorderBottom(1).BorderRight(1).Padding(2).Text("CEP: 12345-678");
                        table.Cell().BorderBottom(1).BorderRight(1).Padding(2).Text("Endereço: Rua Ficticia, 123");
                        table.Cell().BorderBottom(1).BorderRight(1).Padding(2).Text("Cidade: Cidade Ficticia");
                        table.Cell().BorderBottom(1).Padding(2).Text("Estado: Estado Ficticio");
                    });
            });
        });
    }

    private void ComposeFooter(IContainer container)
    {
        container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
        {
            column.Spacing(5);
            column.Item().Text("Informações Adicionais").FontSize(14);
            column.Item().Text("Lorem ipsum dolor sit amet, consectetur adipiscing elit. In in sollicitudin leo, in imperdiet nibh. " +
                               "In non sem ac magna fermentum ullamcorper. Nullam tempus orci ultrices, vehicula arcu ullamcorper, gravida ante." +
                               " Suspendisse nunc eros, sodales et lectus finibus, tincidunt maximus.");
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Table(table =>
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
                var totalPrice = invoice.Products.Sum(product => product.UnitPrice * product.Quantity);
                column.Item().AlignRight().BorderTop(2).Text($"Total Price: {totalPrice:C}").FontSize(14);
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
            page.Content().Element(ComposeContent);
            page.Footer().Element(ComposeFooter);
        });
    }
}