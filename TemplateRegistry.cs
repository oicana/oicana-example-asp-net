namespace Oicana.Example;

/// <summary>
/// A registry for the used version of a template
/// </summary>
public static class TemplateRegistry
{
    /// <summary>
    /// The registered templates and their versions
    /// </summary>
    public static readonly Dictionary<string, TemplateVersion> Registry = new Dictionary<string, TemplateVersion>();

    /// <summary>
    /// Add a template to the registry with the given version
    /// </summary>
    /// <param name="template"></param>
    /// <param name="version"></param>
    public static void Add(string template, TemplateVersion version)
    {
        Registry.Add(template, version);
    }
}

/// <summary>
/// The semantic version of a template
/// </summary>
public class TemplateVersion
{
    /// <summary>
    /// Create a semantic version from the three version parts
    /// </summary>
    /// <param name="major"></param>
    /// <param name="minor"></param>
    /// <param name="patch"></param>
    /// <returns></returns>
    public static TemplateVersion From(uint major, uint minor, uint patch)
    {
        return new TemplateVersion()
        {
            Major = major,
            Minor = minor,
            Patch = patch
        };
    }

    private uint Major { init; get; }
    private uint Minor { init; get; }
    private uint Patch { init; get; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{Major}.{Minor}.{Patch}";
    }
}