using Domain.Services.Abstractions;

namespace Domain.Services.Documents;

public class S3DocumentStorage : IDocumentStorage
{
    public Task EnsureBucketAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task UploadAsync(string key, Stream content, string contentType, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task<Stream> OpenReadAsync(string key, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task DeleteAsync(string key, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public string GetPresignedDownloadUrl(string key, TimeSpan validFor) => throw new NotImplementedException();
}