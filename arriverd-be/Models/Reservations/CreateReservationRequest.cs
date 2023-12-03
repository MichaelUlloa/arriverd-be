using arriverd_be.Entities;
using System.ComponentModel.DataAnnotations;

namespace arriverd_be.Models.Reservations;

public class CreateReservationRequest
{
    public int? ExcursionId { get; set; }
    [Range(1, int.MaxValue)]
    public short Quantity { get; set; }
    public int? PaymentMethodId { get; set; } = 1;

    internal Reservation ToReservation()
        => new()
        {
            Quantity = Quantity,
        };
}
