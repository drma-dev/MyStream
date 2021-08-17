using MyStream.Resources;
using System.ComponentModel.DataAnnotations;

namespace MyStream.Modal.Enum
{
    public enum DeliveryModel
    {
        [Display(Name = "AVODName", Description = "AVODDescription", ResourceType = typeof(ResourceEnum))]
        AVOD = 1,

        [Display(Name = "SVODName", Description = "SVODDescription", ResourceType = typeof(ResourceEnum))]
        SVOD = 2,

        [Display(Name = "TVODName", Description = "TVODDescription", ResourceType = typeof(ResourceEnum))]
        TVOD = 3,

        [Display(Name = "PVODName", Description = "PVODDescription", ResourceType = typeof(ResourceEnum))]
        PVOD = 4,
    }
}