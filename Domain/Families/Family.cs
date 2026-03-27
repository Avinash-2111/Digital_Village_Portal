using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MyVillageApp.Domain.Families
{
    public class Family
    {
        public int Id { get; set; }
        [Required]
        public string OwnerName { get; set; }

        public string PhoneNumber { get; set; }
        [Required]
        public string RationCardNumber { get; set; }

        public decimal TotalAcres { get; set; }

        public int VehiclesCount { get; set; }

        public bool OwnHouse { get; set; }

        public bool GasConnection { get; set; }
       

        public bool WaterConnection { get; set; }

      
    }
}