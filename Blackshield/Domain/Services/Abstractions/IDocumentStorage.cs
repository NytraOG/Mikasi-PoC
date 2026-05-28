namespace Domain.Services.Abstractions;

public interface IDocumentStorage
{
    Task EnsureBucketAsync(CancellationToken cancellationToken = default);
    Task UploadAsync(string key, Stream content, string contentType, CancellationToken cancellationToken = default);
    Task<Stream> OpenReadAsync(string key, CancellationToken cancellationToken = default);
    Task DeleteAsync(string key, CancellationToken cancellationToken = default);
    string GetPresignedDownloadUrl(string key, TimeSpan validFor);
}