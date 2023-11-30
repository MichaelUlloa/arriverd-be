using arriverd_be.Entities;

namespace arriverd_be.Models.Excursions;

public class ListImageModel
{
    public ListImageModel(Image image)
    {
        Id = image.Id;
        Url = image.Url;
    }

    public int? Id { get; set; }
    public string? Url { get; set; }
}
