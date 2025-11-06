namespace Oicana.Example.Models;

/// <summary>
/// Payload to create a certificate
/// </summary>
/// <example>
/// {
///    "name": "Jane Doe"
/// }
/// </example>
public class CreateCertificate
{
    /// <summary>
    /// Name to create the certificate for
    /// </summary>
    /// <example>Jane Doe</example>
    public required string Name { get; set; }
}
