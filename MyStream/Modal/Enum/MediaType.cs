using System.ComponentModel.DataAnnotations;

namespace MyStream.Modal.Enum
{
    public enum MediaType
    {
        [Display(Name = "movieName", ResourceType = typeof(Resources.Enum.MediaType))]
        movie = 1,

        [Display(Name = "tvName", ResourceType = typeof(Resources.Enum.MediaType))]
        tv = 2
    }
}