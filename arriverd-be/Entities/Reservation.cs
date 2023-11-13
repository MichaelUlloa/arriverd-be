namespace arriverd_be.Entities;

public class Reservation
{
    public int? Id { get; set; }
    public Excursion? Excursion { get; set; }
    public short? Quantity { get; set; }

    public List<Schedule> Schedules { get; set; } = new();
}
