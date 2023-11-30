using Azure.Identity;
using Azure.Storage.Blobs;

namespace arriverd_be;


public class BlobService : IBlobService
{
    private readonly Uri _blobStorageUri;

    public BlobService(IConfiguration configuration)
    {
        string blobStorageUri = configuration["BlobStorageUri"] ?? throw new Exception("BlobStorageUri is empty on appsettings.json");
        _blobStorageUri = new Uri(blobStorageUri);
    }

    public async Task<ImageResponse> UploadImageAsync(byte[] data)
    {
        BlobServiceClient client = new(_blobStorageUri, null);

        var container = client.GetBlobContainerClient("data");

        var binaryData = new BinaryData(data);

        var id = Guid.NewGuid();
        string imageId = $"image-{id}.jpg";
        await container.UploadBlobAsync(imageId, binaryData);

        return new($"https://{client.Uri.Host}/data/{imageId}", id);
    }

    public async Task DeleteImageAsync(Guid id)
    {
        BlobServiceClient client = new(_blobStorageUri, null);

        var container = client.GetBlobContainerClient("data");

        await container.DeleteBlobAsync($"image-{id}.jpg");
    }
}
