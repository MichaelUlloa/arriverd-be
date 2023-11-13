using System.ComponentModel.DataAnnotations;

namespace arriverd_be.Models.Excursions;

public class CreateImageRequest
{
    [Required]
    public byte[] Data { get; set; }
}
