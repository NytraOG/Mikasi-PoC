using Microsoft.AspNetCore.Components.Forms;

namespace Domain.Viewmodels;

public class BrowserFileViewmodel(string name, DateTimeOffset lastModified, long size, string contentType, Stream stream)
        : IBrowserFile
{
    public Stream Stream { get; } = stream;

    public Stream OpenReadStream(long maxAllowedSize = Konstanten.MaxDocumentBytes, CancellationToken cancellationToken = default) => Stream;

    public string         Name         { get; } = name;
    public DateTimeOffset LastModified { get; } = lastModified;
    public long           Size         { get; } = size;
    public string         ContentType  { get; } = contentType;
}