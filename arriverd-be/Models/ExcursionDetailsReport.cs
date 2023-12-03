namespace arriverd_be.Models;

public class ExcursionDetailsReport
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public int? Reservations { get; set; }
    public decimal? AmountPaid { get; set; }
    public string? PaymentMethod { get; set; }
}
