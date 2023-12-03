using arriverd_be.Entities;

namespace arriverd_be.Models;

public class ExcursionReservationReport
{
    public ExcursionReservationReport(Excursion excursion, int? totalReservations)
    {
        Excursion = new()
        {
            Name = excursion.Name,
            Departure = excursion.Departure,
            Capacity = excursion.Capacity,
        };
        TotalReservations = totalReservations;

        decimal? percent = totalReservations / (decimal)excursion.Capacity * 100;
        TotalSoldPercent = (int?)Math.Round((decimal)percent);
    }

    public ExcursionModel? Excursion { get; set; }
    public int? TotalReservations { get; set; }
    public int? TotalSoldPercent { get; set; }

    public class ExcursionModel
    {
        public string? Name { get; set; }
        public DateTime? Departure { get; set; }
        public short? Capacity { get; set; }
    }
}
