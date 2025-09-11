namespace Oicana.Example.Services;

/// <summary>
/// A service that can store and retrieve blobs
/// </summary>
public interface IStoredBlobService
{
    /// <summary>
    /// Store the given bytes as a blob
    /// </summary>
    /// <param name="blob">byte content of the blob</param>
    /// <returns>identifier to retrieve the blob with</returns>
    public Task<Guid> StoreBlob(Stream blob);

    /// <summary>
    /// Retrieve the byte content of the blob associated with the given identifier
    /// </summary>
    /// <param name="blobId">identifier of the blob to retrieve</param>
    /// <returns>blob byte content; null if blob could not be retrieved</returns>
    public Task<byte[]?> RetrieveBlob(Guid blobId);
}