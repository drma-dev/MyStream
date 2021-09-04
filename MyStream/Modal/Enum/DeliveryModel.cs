using System.ComponentModel.DataAnnotations;

namespace MyStream.Modal.Enum
{
    public enum DeliveryModel
    {
        [Display(Name = "FREEName", Description = "FREEDescription", ResourceType = typeof(Resources.Enum.DeliveryModel))]
        FREE = 0,

        [Display(Name = "AVODName", Description = "AVODDescription", ResourceType = typeof(Resources.Enum.DeliveryModel))]
        AVOD = 1,

        [Display(Name = "SVODName", Description = "SVODDescription", ResourceType = typeof(Resources.Enum.DeliveryModel))]
        SVOD = 2,

        [Display(Name = "TVODName", Description = "TVODDescription", ResourceType = typeof(Resources.Enum.DeliveryModel))]
        TVOD = 3,

        [Display(Name = "PVODName", Description = "PVODDescription", ResourceType = typeof(Resources.Enum.DeliveryModel))]
        PVOD = 4,
    }
}