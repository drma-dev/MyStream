using MyStream.Modal.Enum;
using System;
using System.Globalization;
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
                Enum.TryParse(typeof(Region), RegionInfo.CurrentRegion.Name, out object region);
                Enum.TryParse(typeof(Language), CultureInfo.CurrentCulture.Name.Replace("-", ""), out object language);

                Region = (Region?)region ?? Region.US;
                Language = (Language?)language ?? Language.enUS;
            }
            else
            {
                Region = sett.Region;
                Language = sett.Language;
            }
        }
    }
}