using arriverd_be.Entities;

namespace arriverd_be.Models.Reservations;

public class CreateReservationRequest
{
    public int? ExcursionId { get; set; }
    public short? Quantity { get; set; }

    internal Reservation ToReservation()
        => new()
        {
            Quantity = Quantity,
        };
}
