using System.Diagnostics.CodeAnalysis;

namespace CallBackBot.Options
{
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class VkOptions
    {
        public string AccessToken { get; set; }

        public string Confirmation { get; set; }
    }
}