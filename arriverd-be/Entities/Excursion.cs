namespace arriverd_be.Entities;

public class Excursion
{
    public int? Id { get; set; }
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

    public List<Schedule> Schedules { get; set; } = new();
    public List<Image> Images { get; set; } = new();
}
