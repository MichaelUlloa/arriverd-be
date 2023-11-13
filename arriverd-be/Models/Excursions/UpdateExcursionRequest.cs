using System.ComponentModel.DataAnnotations;

namespace arriverd_be.Models.Excursions;

public class UpdateExcursionRequest : IValidatableObject
{
    public string? Name { get; set; }
    public string? Destination { get; set; }
    public string? DepartureLocation { get; set; }
    public string? DestinationLocation { get; set; }
    [Range(1, short.MaxValue)]
    public short? MaxReservations { get; set; }
    [Range(1, short.MaxValue)]
    public short? MinReservations { get; set; }
    public decimal? Price { get; set; }
    public string? Description { get; set; }
    public string? EquipmentDetails { get; set; }
    public bool? IsPublic { get; set; }
    public bool? IsActive { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // We're using IValidatableObject instead of Range because as of now, Range isn't working propertly.
        if (Price < 0)
            yield return new ValidationResult($"{nameof(Price)} must be between 1 and {decimal.MaxValue}");
    }
}
