using Oicana.Example.Models;

namespace Oicana.Example.Services;

/// <summary>
/// Service for creating certificates
/// </summary>
public interface ICertificateService
{
    /// <summary>
    /// Create a certificate for the given data
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Compiled certificate PDf file</returns>
    Task<Stream?> CreateCertificate(CreateCertificate request);
}