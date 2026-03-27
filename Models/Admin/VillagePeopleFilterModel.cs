using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyVillageApp.Models.Admin
{
    public class VillagePeopleFilterModel
    {
        public string SearchText { get; set; }
        public string OwnerName { get; set; }

        public int? ProfessionId { get; set; }
        public int? SchemeId { get; set; }
        public int? PensionId { get; set; }
        public int? VehicleTypeId { get; set; }

        public bool Male { get; set; }
        public bool Female { get; set; }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool? IsDisabled { get; set; }
        public bool? IsMarried { get; set; }
        public List<SelectListItem> AvailableProfessions { get; set; } = new();
        public List<SelectListItem> AvailableSchemes { get; set; } = new();
        public List<SelectListItem> AvailablePensions { get; set; } = new();
        public List<SelectListItem> AvailableVehicleTypes { get; set; } = new();

        public List<VillagePeopleListModel> Results { get; set; } = new();
    }
}