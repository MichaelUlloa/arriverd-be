namespace arriverd_be.Entities;

public class Image
{
    public int? Id { get; set; }
    public byte[]? Data { get; set; }
    public int? ExcursionId { get; set; }
}
