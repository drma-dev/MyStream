using MyStream.Modal.Enum;
using System.Text.Json.Serialization;

namespace MyStream.Core
{
    public class Settings
    {
        public Region Region { get; set; }
        public Language Language { get; set; }

        [JsonConstructor]
        public Settings() { }

        public Settings(IStorageService StorageService)
        {
            var sett = StorageService.Local.GetItem<Settings>("Settings");

            if (sett == null)
            {
                Region = Region.BR;
                Language = Language.ptBR;
            }
            else
            {
                Region = sett.Region;
                Language = sett.Language;
            }
        }
    }
}