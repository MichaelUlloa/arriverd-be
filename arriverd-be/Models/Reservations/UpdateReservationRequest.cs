using arriverd_be.Entities;
using System.ComponentModel.DataAnnotations;

namespace arriverd_be.Models.Reservations;

public class UpdateReservationRequest
{
    public int? ExcursionId { get; set; }
    [Range(1, int.MaxValue)]
    public short? Quantity { get; set; }

    internal Reservation ToReservation()
        => new()
        {
            Quantity = Quantity,
        };
}
