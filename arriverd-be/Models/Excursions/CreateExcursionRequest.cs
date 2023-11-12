using arriverd_be.Entities;

namespace arriverd_be.Models.Excursions;

public class CreateExcursionRequest
{
    public string? Name { get; set; }
    public string? Destination { get; set; }
    public string? DepartureLocation { get; set; }
    public string? DestinationLocation { get; set; }
    public short? MaxReservations { get; set; }
    public short? MinReservations { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    public string? EquipmentDetails { get; set; }
    public bool? IsPublic { get; set; }
    public bool? IsActive { get; set; }

    internal Excursion ToExcursion()
        => new()
        {
            Name = Name,
            Destination = Destination,
            DepartureLocation = DepartureLocation,
            DestinationLocation = DestinationLocation,
            MaxReservations = MaxReservations,
            MinReservations = MinReservations,
            Price = Price,
            Description = Description,
            EquipmentDetails = EquipmentDetails,
            IsPublic = IsPublic,
            IsActive = IsActive,
        };
}
