using MyStream.Resources;
using System.ComponentModel.DataAnnotations;

namespace MyStream.Modal.Enum
{
    public enum MediaType
    {
        [Display(Name = "movieName", ResourceType = typeof(ResourceEnum))]
        movie = 1,

        [Display(Name = "tvName", ResourceType = typeof(ResourceEnum))]
        tv = 2
    }
}