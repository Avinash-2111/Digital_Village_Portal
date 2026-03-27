using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MyVillageApp.Models.Admin
{
    public class FamilyMemberModel
    {
        public int Id { get; set; }

        public int FamilyId { get; set; }

        [Required]
        public string Name { get; set; }

        public string AadhaarNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public List<int> SelectedProfessionIds { get; set; } = new();
        public List<int> SelectedSchemeIds { get; set; } = new();
        public List<int> SelectedPensionIds { get; set; } = new();
        public List<int> SelectedVehicleTypeIds { get; set; } = new();

        public bool IsPhysicallyDisabled { get; set; }

        public bool IsMarried { get; set; }

        public IList<SelectListItem> AvailableProfessions { get; set; } = new List<SelectListItem>();
        public IList<SelectListItem> AvailableSchemes { get; set; } = new List<SelectListItem>();
        public IList<SelectListItem> AvailablePensions { get; set; } = new List<SelectListItem>();
        public IList<SelectListItem> AvailableVehicleTypes { get; set; } = new List<SelectListItem>();
    }
}