namespace arriverd_be;

public record ImageResponse(string ImageUri, Guid ImageId);

public interface IBlobService
{
    Task<ImageResponse> UploadImageAsync(byte[] data);
    Task DeleteImageAsync(Guid id);
}
