using System.ComponentModel.DataAnnotations;

namespace arriverd_be.Models.Reservations;

public class CreateImageRequest
{
    [Required]
    public byte[] Data { get; set; }
}
