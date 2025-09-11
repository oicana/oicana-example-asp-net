namespace Oicana.Example.Services;

/// <summary>
/// This is a cheap example implementation of a service that can store and retrieve files.
///
/// It saves and reads from the local `blobs` directory.
/// In production software, you should store files differently (e.g. in a cloud container or a database). 
/// </summary>
public class StoredBlobService : IStoredBlobService
{
    private readonly string _directory = "blobs";

    /// <inheritdoc />
    public async Task<Guid> StoreBlob(Stream blob)
    {
        var id = Guid.NewGuid();
        await using var fileStream = File.Create($"{_directory}/{id}");
        await blob.CopyToAsync(fileStream);
        return id;
    }

    /// <inheritdoc />
    public async Task<byte[]?> RetrieveBlob(Guid blobId)
    {
        if (!File.Exists($"{_directory}/{blobId}")) return null;

        return await File.ReadAllBytesAsync($"{_directory}/{blobId}");
    }
}
