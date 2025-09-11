using Oicana.Config;
using Oicana.Example.Controllers;
using Oicana.Inputs;
using Oicana.Template;

namespace Oicana.Example.Services;

/// <inheritdoc />
public class TemplatingService(IOicanaService oicanaService, IStoredBlobService storedBlobService, ILogger<TemplatingService> logger) : ITemplatingService
{
    /// <inheritdoc />
    public async Task<Stream?> Compile(string templateId, IList<TemplateJsonInput> jsonInput, IList<StoredBlobInput> storedBlobInputs)
    {
        var template = oicanaService.GetTemplate(templateId);
        if (template == null)
        {
            return null;
        }
        var blobInputs = await LoadBlobInputs(storedBlobInputs);

        return await Task.Run<Stream?>(() => template.Compile(jsonInput, blobInputs, CompilationOptions.Pdf()));
    }

    /// <inheritdoc />
    public async Task<Stream?> Preview(string templateId, IList<TemplateJsonInput> jsonInput, IList<StoredBlobInput> storedBlobInputs)
    {
        var template = oicanaService.GetTemplate(templateId);
        if (template == null)
        {
            return null;
        }
        var blobInputs = await LoadBlobInputs(storedBlobInputs);

        return await Task.Run<Stream?>(() => template.Compile(jsonInput, blobInputs, CompilationOptions.Png(1.0f)));
    }

    /// <inheritdoc />
    public bool RemoveTemplate(string templateId)
    {
        var template = oicanaService.RemoveTemplate(templateId);
        return template != null;
    }

    private async Task<IList<TemplateBlobInput>> LoadBlobInputs(IList<StoredBlobInput> storedBlobInputs)
    {
        var blobInputs = new List<TemplateBlobInput>();
        foreach (var storedBlobInput in storedBlobInputs)
        {
            var blob = await storedBlobService.RetrieveBlob(storedBlobInput.BlobId);
            if (blob == null)
            {
                logger.LogWarning($"The stored blob {storedBlobInput.BlobId} could not be loaded.");
                continue;
            }
            blobInputs.Add(new TemplateBlobInput(storedBlobInput.Key, blob));
        }

        return blobInputs;
    }
}
