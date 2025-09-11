namespace Oicana.Example;

/// <summary>
/// Templating related extensions for the application builder
/// </summary>
public static class TemplatingExtension
{
    /// <summary>
    /// Register the given template version
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="template"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    public static IHostApplicationBuilder RegisterTemplate(
        this IHostApplicationBuilder builder, string template, TemplateVersion version)
    {
        TemplateRegistry.Add(template, version);
        return builder;
    }
}
