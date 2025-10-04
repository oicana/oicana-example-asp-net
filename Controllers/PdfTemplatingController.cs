using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Oicana.Example.Services;
using Oicana.Inputs;
using System.Text.RegularExpressions;

namespace Oicana.Example.Controllers;

/// <summary>
/// Manage and compile PDF templates
/// </summary>
/// <param name="logger"></param>
/// <param name="templatingService"></param>
[ApiController]
[Route("templates")]
public class PdfTemplatingController(ILogger<PdfTemplatingController> logger, ITemplatingService templatingService) : ControllerBase
{
    /// <summary>
    /// Compile a template with given input
    /// </summary>
    /// <param name="template" example="table"></param>
    /// <param name="request"></param>
    /// <returns>The compiled PDF document</returns>
    [HttpPost("{template}/compile")]
    public async Task<IActionResult> CompilePdfTemplate([FromRoute] String template, [FromBody] CompilePdfRequest request)
    {
        try
        {
            var watch = new Stopwatch();
            watch.Start();
            var stream = await templatingService.Compile(template, request.JsonInputs, request.BlobInputs);
            if (stream == null)
            {
                return StatusCode(404);
            }
            watch.Stop();
            var file = new FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = $"{template}_{DateTimeOffset.Now:yyyy_MM_dd_HH_mm_ss_ffff}.pdf"
            };

            logger.LogInformation("PDF generated in {ElapsedMilliseconds}ms", watch.ElapsedMilliseconds);
            return file;
        }
        catch (Exception e)
        {
            string unescapedMessage = Regex.Unescape(e.Message);
            logger.LogError(unescapedMessage);
            return StatusCode(400);
        }
    }
    /// <summary>
    /// Compile a template with given input
    /// </summary>
    /// <param name="template" example="table"></param>
    /// <param name="request"></param>
    /// <returns>The compiled document as a png file</returns>
    [HttpPost("{template}/preview")]
    public async Task<IActionResult> PreviewPngTemplate([FromRoute] String template, [FromBody] CompilePdfRequest request)
    {
        try
        {
            var watch = new Stopwatch();
            watch.Start();
            var stream = await templatingService.Preview(template, request.JsonInputs, request.BlobInputs);
            if (stream == null)
            {
                return StatusCode(404);
            }
            watch.Stop();
            var file = new FileStreamResult(stream, "image/png")
            {
                FileDownloadName = $"{template}_{DateTimeOffset.Now:yyyy_MM_dd_HH_mm_ss_ffff}.png"
            };

            logger.LogInformation("PNG generated in {ElapsedMilliseconds}ms", watch.ElapsedMilliseconds);
            return file;
        }
        catch (Exception e)
        {
            string unescapedMessage = Regex.Unescape(e.Message);
            logger.LogError(unescapedMessage);
            return StatusCode(400);
        }
    }

    /// <summary>
    /// Reset the cache for the given template
    /// </summary>
    /// <param name="template" example="table"></param>
    [HttpPost("{template}/reset")]
    public IActionResult ResetTemplate([FromRoute] String template)
    {
        var success = templatingService.RemoveTemplate(template);
        return success ? StatusCode(204) : StatusCode(404);
    }
}

/// <summary>
/// Request to compile a template with given input
/// </summary>
/// <example>
/// {
///    "input": [
///         {
///             "key": "input",
///             "value": {
///                "description": "from sample data",
///                "rows": [
///                    {
///                        "name": "Frank",
///                        "one": "first",
///                        "two": "second",
///                        "three": "third"
///                    },
///                    {
///                        "name": "John",
///                        "one": "first_john",
///                        "two": "second_john",
///                        "three": "third_john"
///                    }
///                ]
///             }
///         }
///     ]
/// }
/// </example>
public class CompilePdfRequest
{
    /// <summary>
    /// Input json to compile the template with
    /// </summary>
    /// <example>
    /// [
    ///     {
    ///         "key": "data",
    ///         "value": {
    ///            "description": "from sample data",
    ///            "rows": [
    ///                {
    ///                    "name": "Frank",
    ///                    "one": "first",
    ///                    "two": "second",
    ///                    "three": "third"
    ///                },
    ///                {
    ///                    "name": "John",
    ///                    "one": "first_john",
    ///                    "two": "second_john",
    ///                    "three": "third_john"
    ///                }
    ///            ]
    ///         }
    ///     }
    /// ]
    /// </example>
    public required IList<TemplateJsonInput> JsonInputs { get; init; }

    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// [
    ///     {
    ///         "key": "logo",
    ///         "blobId": "00000000-0000-0000-0000-000000000000"
    ///     }
    /// ]
    /// </example>
    public required IList<StoredBlobInput> BlobInputs { get; init; }
}

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
