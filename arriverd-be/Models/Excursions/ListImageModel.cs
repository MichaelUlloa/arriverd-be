using arriverd_be.Entities;

namespace arriverd_be.Models.Excursions;

public class ListImageModel
{
    public ListImageModel(Image image)
    {
        Id = image.Id;
        Url = image.Url;
        Order = image.Order;
    }

    public int? Id { get; set; }
    public string? Url { get; set; }
    public int? Order { get; set; }
}
