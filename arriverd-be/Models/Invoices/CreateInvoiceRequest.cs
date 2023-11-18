using arriverd_be.Entities;

namespace arriverd_be.Models.Invoices;

public class CreateInvoiceRequest
{
    public int? ReservationId { get; set; }
    public DateTime? Date { get; set; }
    public int? PaymentMethodId { get; set; }
    public decimal? TotalAmount { get; set; }

    internal Invoice ToInvoice()
        => new()
        {
            Date = Date,
            TotalAmount = TotalAmount,
        };
}
