namespace arriverd_be.Entities;

public class Image
{
    public int? Id { get; set; }
    public int? ExcursionId { get; set; }
    public Excursion? Excursion { get; set; }
    public string? Url { get; set; }
    public Guid? ImageId { get; set; }
    public int? Order { get; set; }
}
