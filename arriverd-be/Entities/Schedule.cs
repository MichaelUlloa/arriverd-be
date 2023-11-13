namespace arriverd_be.Entities;

public class Schedule
{
    public int Id { get; set; }
    public int? ReservationId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public short? Seats { get; set; }
    public string? Itinerary { get; set; }
}
