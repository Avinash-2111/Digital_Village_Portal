using System.ComponentModel.DataAnnotations;

namespace MyVillageApp.Domain.VehicleTypes
{
    public class VehicleType
    {
        public int Id { get; set; }
        [Required]
        public string VehicleTypeName { get; set; }
    }
}