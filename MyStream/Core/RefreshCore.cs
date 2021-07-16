using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MyStream.Core
{
    public static class RefreshCore
    {
        public static EventCallback<string> RegionChanged { get; set; }

        public static EventCallback<string> LanguageChanged { get; set; }

        public static async Task ChangeRegion(string value)
        {
            await RegionChanged.InvokeAsync(value);
        }

        public static async Task ChangeLanguage(string value)
        {
            await LanguageChanged.InvokeAsync(value);
        }
    }
}