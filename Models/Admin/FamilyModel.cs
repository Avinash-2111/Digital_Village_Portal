using Microsoft.AspNetCore.Mvc.Rendering;
using MyVillageApp.Models.Admin;

namespace MyVillageApp.Models.Admin
{
    public class FamilyModel
    {
        //public class FamilyPetInputModel
        //{
        //    public int PetId { get; set; }
        //    public int PetCount { get; set; }
        //}

        public int Id { get; set; }

        public string OwnerName { get; set; }

        public string PhoneNumber { get; set; }

        public string RationCardNumber { get; set; }

        public decimal? TotalAcres { get; set; }

        public int? VehiclesCount { get; set; }

        public bool OwnHouse { get; set; }

        public bool GasConnection { get; set; }

        public bool WaterConnection { get; set; }

        public List<FamilyPetInputModel> Pets { get; set; } = new();

        public IList<SelectListItem> AvailablePets { get; set; } = new List<SelectListItem>();
    }
}