using arriverd_be.Entities;
using System.ComponentModel.DataAnnotations;

namespace arriverd_be.Models.Excursions;

public class CreateFAQRequest
{
    [Required]
    [StringLength(80)]
    public string? Question { get; set; }
    [Required]
    public string? Answer { get; set; }

    internal FAQ ToFAQ()
        => new()
        {
            Question = Question,
            Answer = Answer,
        };
}
