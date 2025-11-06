using Oicana.Inputs;

namespace Oicana.Example.Models;

/// <summary>
/// Request to compile a template with given input
/// </summary>
/// <example>
/// {
///    "jsonInputs": [
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