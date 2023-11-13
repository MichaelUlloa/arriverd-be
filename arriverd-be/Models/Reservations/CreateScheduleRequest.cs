using arriverd_be.Entities;
using System.ComponentModel.DataAnnotations;

namespace arriverd_be.Models.Reservations;

public class CreateScheduleRequest
{
    [Required]
    public DateTime? StartDate { get; set; }
    [Required]
    public DateTime? EndDate { get; set; }
    [Required]
    [Range(1, short.MaxValue)]
    public short? Seats { get; set; }
    [Required]
    public string? Itinerary { get; set; }

    internal Schedule ToSchedule()
        => new()
        {
            StartDate = StartDate,
            EndDate = EndDate,
            Seats = Seats,
            Itinerary = Itinerary,
        };
}
