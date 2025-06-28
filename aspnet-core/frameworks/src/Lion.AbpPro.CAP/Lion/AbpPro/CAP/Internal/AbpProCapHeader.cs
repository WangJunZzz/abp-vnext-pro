using System.Collections.ObjectModel;

namespace Lion.AbpPro.CAP.Internal;


public class AbpProCapHeader : ReadOnlyDictionary<string, string>
{
    internal IDictionary<string, string> ResponseHeader { get; set; }

    public AbpProCapHeader(IDictionary<string, string> dictionary) : base(dictionary)
    {
    }

    /// <summary>
    /// When a callbackName is specified from publish message, use this method to add an additional header.
    /// </summary>
    /// <param name="key">The response header key.</param>
    /// <param name="value">The response header value.</param>
    public void AddResponseHeader(string key, string value)
    {
        ResponseHeader ??= new Dictionary<string, string>();
        ResponseHeader[key] = value;
    }

    /// <summary>
    /// When a callbackName is specified from publish message, use this method to abort the callback.
    /// </summary>
    public void RemoveCallback()
    {
        Dictionary.Remove(Headers.CallbackName);
    }

    /// <summary>
    /// When a callbackName is specified from Publish message, use this method to rewrite the callback name.
    /// </summary>
    /// <param name="callbackName">The new callback name.</param>
    public void RewriteCallback(string callbackName)
    {
        Dictionary[Headers.CallbackName] = callbackName;
    }
}