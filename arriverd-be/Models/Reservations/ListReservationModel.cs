using arriverd_be.Entities;

namespace arriverd_be.Models.Reservations;

public class ListReservationModel
{
    public ListReservationModel(Reservation reservation)
    {
        Id = reservation.Id;
        Excursion = new()
        {
            Id = reservation.Excursion?.Id,
            Name = reservation.Excursion?.Name,
            Meeting = reservation.Excursion?.Meeting,
            Departure = reservation.Excursion?.Departure,
            Return = reservation.Excursion?.Return,
            Destination = reservation.Excursion?.Destination,
            DepartureLocation = reservation.Excursion?.DepartureLocation,
            DestinationLocation = reservation.Excursion?.DestinationLocation,
            Capacity = reservation.Excursion?.Capacity,
            AvailableSeats = reservation.Excursion?.AvailableSeats,
            Price = reservation.Excursion?.Price,
            Description = reservation.Excursion?.Description,
            EquipmentDetails = reservation.Excursion?.EquipmentDetails,
            IsPublic = reservation.Excursion?.IsPublic,
            IsActive = reservation.Excursion?.IsActive,
        };
        PaymentMethod = new()
        {
            Id = reservation.PaymentMethod?.Id,
            Name = reservation.PaymentMethod?.Name,
        };
        Quantity = reservation.Quantity;
        UserId = reservation.UserId;
    }

    public int? Id { get; set; }
    public ExcursionModel? Excursion { get; set; }
    public PaymentMethodModel? PaymentMethod { get; set; }
    public short? Quantity { get; set; }
    public string? UserId { get; set; }

    public class ExcursionModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Meeting { get; set; }
        public DateTime? Departure { get; set; }
        public DateTime? Return { get; set; }
        public string? Destination { get; set; }
        public string? DepartureLocation { get; set; }
        public string? DestinationLocation { get; set; }
        public short? Capacity { get; set; }
        public short? AvailableSeats { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? EquipmentDetails { get; set; }
        public bool? IsPublic { get; set; }
        public bool? IsActive { get; set; }
    }

    public class PaymentMethodModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
    }
}
