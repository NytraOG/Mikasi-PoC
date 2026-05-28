namespace Domain.Exceptions;

public sealed class StorageObjectNotFoundException(string key)
        : Exception($"Dokument '{key}' nicht gefunden.")
{
    public string Key { get; } = key;
}