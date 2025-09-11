using Microsoft.AspNetCore.Mvc;
using Oicana.Example.Services;

namespace Oicana.Example.Controllers;

/// <summary>
/// Upload blobs to the service to use as template inputs
/// </summary>
[ApiController]
[Route("blobs")]
public class StoredBlobController(IStoredBlobService storedBlobService) : ControllerBase
{
    /// <summary>
    /// Store the given file as a blob input for later use in templates
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> StoreBlob(IFormFile file)
    {
        var id = await storedBlobService.StoreBlob(file.OpenReadStream());
        return Ok(id);
    }
}
