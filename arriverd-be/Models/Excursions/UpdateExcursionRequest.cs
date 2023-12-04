using System.ComponentModel.DataAnnotations;

namespace arriverd_be.Models.Excursions;

public class UpdateExcursionRequest : IValidatableObject
{
    [Required]
    public string? Name { get; set; }
    public ScheduleModel Schedule { get; set; } = new();
    public string? Destination { get; set; }
    public string? DepartureLocation { get; set; }
    public string? DestinationLocation { get; set; }
    [Required]
    [Range(1, short.MaxValue)]
    public short Capacity { get; set; }
    [Required]
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? EquipmentDetails { get; set; }
    public bool? IsPublic { get; set; }
    public bool IsActive { get; set; } = true;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // We're using IValidatableObject instead of Range because as of now, Range isn't working propertly.
        if (Price < 0)
            yield return new ValidationResult($"{nameof(Price)} must be between 1 and {decimal.MaxValue}");

        if (Schedule.Meeting > Schedule.Departure)
            yield return new ValidationResult($"{nameof(Schedule)}.{nameof(Schedule.Meeting)} must be before {nameof(Schedule)}.{nameof(Schedule.Departure)}");

        if (Schedule.Departure > Schedule.Return)
            yield return new ValidationResult($"{nameof(Schedule)}.{nameof(Schedule.Departure)} must be before {nameof(Schedule)}.{nameof(Schedule.Return)}");
    }

    public class ScheduleModel
    {
        public DateTime? Meeting { get; set; }
        public DateTime? Departure { get; set; }
        public DateTime? Return { get; set; }
    }
}
