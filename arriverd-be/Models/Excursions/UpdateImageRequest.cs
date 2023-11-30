using System.ComponentModel.DataAnnotations;

namespace arriverd_be.Models.Excursions;

public class UpdateImageRequest
{
    [Required]
    [Range(0, int.MaxValue)]
    public int Order { get; set; }
}
