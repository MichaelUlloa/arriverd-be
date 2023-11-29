namespace arriverd_be.Entities;

public class Excursion
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
    public short AvailableSeats { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    public string? EquipmentDetails { get; set; }
    public bool? IsPublic { get; set; }
    public bool? IsActive { get; set; }

    public List<Image> Images { get; set; } = new();
    public List<FAQ> FAQs { get; set; } = new();
    public List<Service> Services { get; set; } = new();
}
