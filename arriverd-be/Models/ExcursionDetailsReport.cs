using arriverd_be.Entities;

namespace arriverd_be.Models;

public class ExcursionDetailsReport
{
    public ExcursionDetailsReport(Reservation reservation)
    {
        Name = reservation.Excursion?.Name;
        Phone = reservation.User?.PhoneNumber;
        Email = reservation.User?.Email;
        Reservations = reservation.Quantity;
        AmountPaid = reservation.Quantity * reservation.Excursion?.Price;
        PaymentMethod = reservation.PaymentMethod?.Name;
    }

    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public int? Reservations { get; set; }
    public decimal? AmountPaid { get; set; }
    public string? PaymentMethod { get; set; }
}
