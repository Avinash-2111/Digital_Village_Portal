using System.ComponentModel.DataAnnotations;

namespace MyVillageApp.Domain.Pets
{
    public class Pet
    {
        public int Id { get; set; }
        [Required]
        public string PetName { get; set; }
    }
}