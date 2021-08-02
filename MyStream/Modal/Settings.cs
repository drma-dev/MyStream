using MyStream.Modal.Enum;

namespace MyStream.Modal
{
    public class Settings
    {
        public Region Region { get; set; } = Region.BR;
        public Language Language { get; set; } = Language.ptBR;
        public TypeMedia TypeMedia { get; set; } = TypeMedia.movie;
    }
}