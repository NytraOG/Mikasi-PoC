using Domain.Services.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Domain.Services.Documents;

public class LocalFilesystemDocumentStorage(IConfiguration Configuration) : IDocumentStorage
{
    public Task EnsureBucketAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

    public async Task UploadAsync(string key, Stream content, string contentType, CancellationToken cancellationToken = default)
    {
        var storageDirectory = Configuration.GetValue<string>("storage:LocalBasePath");
        var basePath         = Path.Combine(storageDirectory!, "Dokumente");
        var filePath         = Path.Combine(basePath, key);
        var directory        = Path.GetDirectoryName(filePath);

        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);

        await using var filestream = new FileStream(filePath, new FileStreamOptions
        {
            Mode    = FileMode.Create,
            Access  = FileAccess.Write,
            Share   = FileShare.None,
            Options = FileOptions.Asynchronous
        });

        await content.CopyToAsync(filestream, cancellationToken);
    }

    public Task<Stream> OpenReadAsync(string key, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task DeleteAsync(string key, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public string GetPresignedDownloadUrl(string key, TimeSpan validFor) => throw new NotImplementedException();
}