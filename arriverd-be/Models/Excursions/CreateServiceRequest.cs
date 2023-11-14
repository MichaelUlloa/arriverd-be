using System.ComponentModel.DataAnnotations;

namespace arriverd_be.Models.Excursions;

public class CreateServiceRequest
{
    [Required]
    [StringLength(80)]
    public string? Name { get; set; }
}
