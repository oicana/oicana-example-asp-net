namespace Oicana.Example.Models;

/// <summary>
/// Pass a blob stored in the service as a blob input to the template
/// </summary>
public class StoredBlobInput
{
    /// <summary>
    /// The key of the blob input
    /// </summary>
    public required string Key { get; init; }

    /// <summary>
    /// Identifier of the blob file
    /// </summary>
    public required Guid BlobId { get; init; }
}