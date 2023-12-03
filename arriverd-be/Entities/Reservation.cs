using arriverd_be.Models;

namespace arriverd_be.Entities;

public class Reservation
{
    public int? Id { get; set; }
    public Excursion? Excursion { get; set; }
    public short? Quantity { get; set; }
    public string? UserId { get; set; }
    public User? User { get; set; }
}
