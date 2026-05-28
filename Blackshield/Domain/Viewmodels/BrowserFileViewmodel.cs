using Microsoft.AspNetCore.Components.Forms;

namespace Domain.Viewmodels;

public class BrowserFileViewmodel(string name, DateTimeOffset lastModified, long size, string contentType, byte[] content)
        : IBrowserFile
{
    public byte[] Content { get; } = content;

    public Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default) => new MemoryStream(Content);

    public string         Name         { get; } = name;
    public DateTimeOffset LastModified { get; } = lastModified;
    public long           Size         { get; } = size;
    public string         ContentType  { get; } = contentType;
}