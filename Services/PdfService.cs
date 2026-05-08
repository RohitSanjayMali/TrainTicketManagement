using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using TrainTicketManagement.Models;

namespace TrainTicketManagement.Services
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateTicketPdf(Booking booking)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(40);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    // HEADER
                    page.Header().Column(col =>
                    {
                        col.Item()
                            .Background("#1a73e8")
                            .Padding(15)
                            .Row(row =>
                            {
                                row.RelativeItem()
                                    .Text("Train Ticket Management System")
                                    .FontSize(18)
                                    .Bold()
                                    .FontColor(Colors.White);

                                row.AutoItem()
                                    .AlignRight()
                                    .Text($"PNR: {booking.PNR}")
                                    .FontSize(14)
                                    .Bold()
                                    .FontColor(Colors.Yellow.Lighten1);
                            });
                    });

                    // CONTENT
                    page.Content().PaddingTop(20).Column(col =>
                    {
                        col.Spacing(12);

                        // Journey Details
                        col.Item()
                            .Border(1).BorderColor(Colors.Grey.Lighten2)
                            .Padding(12)
                            .Column(inner =>
                            {
                                inner.Item().Text("Journey Details").Bold().FontSize(14);
                                inner.Item().PaddingTop(8).Row(row =>
                                {
                                    row.RelativeItem().Column(c =>
                                    {
                                        c.Item().Text($"Train Name: {booking.Train.TrainName}").FontSize(12);
                                        c.Item().Text($"Train No: {booking.Train.TrainNumber}");
                                        c.Item().Text($"From: {booking.Train.FromStation?.Name ?? "-"}");
                                        c.Item().Text($"To: {booking.Train.ToStation?.Name ?? "-"}");
                                    });
                                    row.RelativeItem().Column(c =>
                                    {
                                        c.Item().Text($"Booking Date: {booking.BookingDate:dd MMM yyyy}");
                                        c.Item().Text($"Passengers: {booking.TotalPassengers}");
                                        c.Item().Text($"Status: {booking.Payment?.Status ?? "N/A"}");
                                        c.Item().Text($"Amount Paid: {booking.Payment?.Amount:N2}");
                                    });
                                });
                            });

                        // Passenger Table
                        col.Item().Text("Passenger Details").Bold().FontSize(14);
                        col.Item()
                            .Border(1).BorderColor(Colors.Grey.Lighten2)
                            .Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(40);
                                    columns.RelativeColumn(3);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                });

                                // Header row
                                table.Header(header =>
                                {
                                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("#").Bold();
                                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Name").Bold();
                                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Age").Bold();
                                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Gender").Bold();
                                });

                                int sn = 1;
                                foreach (var p in booking.Passengers)
                                {
                                    table.Cell().Padding(5).Text(sn++.ToString());
                                    table.Cell().Padding(5).Text(p.Name);
                                    table.Cell().Padding(5).Text(p.Age.ToString());
                                    table.Cell().Padding(5).Text(p.Gender);
                                }
                            });
                    });

                    // FOOTER
                    page.Footer()
                        .AlignCenter()
                        .PaddingTop(10)
                        .Text(text =>
                        {
                            text.Span("Thank you for booking with Train Ticket Management System  |  ");
                            text.Span($"Printed on: {DateTime.Now:dd MMM yyyy HH:mm}");
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}
