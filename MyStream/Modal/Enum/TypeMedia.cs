using System.ComponentModel.DataAnnotations;

namespace MyStream.Modal.Enum
{
    public enum TypeMedia
    {
        [Display(Name = "Movies")]
        movie,

        [Display(Name = "TV Shows")]
        tv
    }
}