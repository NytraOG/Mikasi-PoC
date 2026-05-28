using Domain.Exceptions;
using Domain.Services.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Domain.Services.Documents;

public class LocalFilesystemDocumentStorage(IConfiguration configuration) : IDocumentStorage
{
    public Task EnsureBucketAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

    public async Task UploadAsync(string key, Stream content, string contentType, CancellationToken cancellationToken = default)
    {
        var filePath = GetFinalDirectory(key, out var directory);

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

    public Task<Stream> OpenReadAsync(string key, CancellationToken cancellationToken = default)
    {
        var filePath = GetFinalDirectory(key, out _);

        if (!File.Exists(filePath))
            throw new StorageObjectNotFoundException(key);

        Stream stream = new FileStream(filePath, new FileStreamOptions
        {
            Mode       = FileMode.Open,
            Access     = FileAccess.Read,
            Share      = FileShare.Read,
            Options    = FileOptions.Asynchronous | FileOptions.SequentialScan,
            BufferSize = 81920
        });

        return Task.FromResult(stream);
    }

    public Task DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        var filePath = GetFinalDirectory(key, out _);

        if (!File.Exists(filePath))
            return Task.CompletedTask;

        File.Delete(filePath);

        return Task.CompletedTask;
    }

    public string GetPresignedDownloadUrl(string key, TimeSpan validFor) => throw new NotImplementedException();

    private string GetFinalDirectory(string key, out string? directory)
    {
        var storageDirectory = configuration.GetValue<string>("storage:LocalBasePath");
        var basePath         = Path.Combine(storageDirectory!, "Dokumente");
        var filePath         = Path.Combine(basePath, key);
        directory = Path.GetDirectoryName(filePath);

        return filePath;
    }
}