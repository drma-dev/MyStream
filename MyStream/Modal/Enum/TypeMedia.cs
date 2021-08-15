using System.ComponentModel.DataAnnotations;

namespace MyStream.Modal.Enum
{
    public enum TypeMedia
    {
        [Display(Name = "Movies", ResourceType = typeof(App))]
        movie,

        [Display(Name = "TVShows", ResourceType = typeof(App))]
        tv
    }
}