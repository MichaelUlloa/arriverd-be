namespace arriverd_be.Entities;

public class Invoice
{
    public int? Id { get; set; }
    public Reservation? Reservation { get; set; }
    public DateTime? Date { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public decimal? TotalAmount { get; set; }
}
