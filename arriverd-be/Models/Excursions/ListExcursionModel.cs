﻿using arriverd_be.Entities;

namespace arriverd_be.Models.Excursions;

public class ListExcursionModel
{
    public ListExcursionModel(Excursion excursion)
    {
        Id = excursion.Id;
        Name = excursion.Name;
        Schedule = new()
        {
            Departure = excursion.Departure,
            Meeting = excursion.Meeting,
            Return = excursion.Return,
        };
        Destination = excursion.Destination;
        DepartureLocation = excursion.DepartureLocation;
        DestinationLocation = excursion.DestinationLocation;
        Capacity = excursion.Capacity;
        AvailableSeats = excursion.AvailableSeats;
        Passengers = (short?)(excursion.Capacity - excursion.AvailableSeats);
        Price = excursion.Price;
        Description = excursion.Description;
        EquipmentDetails = excursion.EquipmentDetails;
        PrimaryImageUrl = excursion.Images?.FirstOrDefault()?.Url;
        IsPublic = excursion.IsPublic;
        IsActive = excursion.IsActive;
    }

    public int? Id { get; set; }
    public string? Name { get; set; }
    public ScheduleModel? Schedule { get; set; }
    public string? Destination { get; set; }
    public string? DepartureLocation { get; set; }
    public string? DestinationLocation { get; set; }
    public short? Capacity { get; set; }
    public short AvailableSeats { get; set; }
    public short? Passengers { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    public string? EquipmentDetails { get; set; }
    public string? PrimaryImageUrl { get; set; }
    public bool? IsPublic { get; set; }
    public bool? IsActive { get; set; }

    public class ScheduleModel
    {
        public DateTime? Meeting { get; set; }
        public DateTime? Departure { get; set; }
        public DateTime? Return { get; set; }
    }
}
