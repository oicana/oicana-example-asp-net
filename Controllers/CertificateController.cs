using Microsoft.AspNetCore.Mvc;
using Oicana.Example.Models;
using Oicana.Example.Services;

namespace Oicana.Example.Controllers;

/// <summary>
/// Create certificates
/// </summary>
/// <param name="certificateService"></param>
[ApiController]
[Route("certificates")]
public class CertificateController(ICertificateService certificateService) : ControllerBase
{
    /// <summary>
    /// Create a Certificate
    /// </summary>
    /// <param name="request"></param>
    /// <returns>The compiled PDF certificate</returns>
    [HttpPost]
    public async Task<IActionResult> CompilePdfTemplate([FromBody] CreateCertificate request)
    {
        var certificate = await certificateService.CreateCertificate(request);
        if (certificate == null)
        {
            return StatusCode(500);
        }

        var file = new FileStreamResult(certificate, "application/pdf")
        {
            FileDownloadName = $"certificate_{DateTimeOffset.Now:yyyy_MM_dd_HH_mm_ss_ffff}.pdf"
        };

        return file;
    }
}