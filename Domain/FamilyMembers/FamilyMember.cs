using System.ComponentModel.DataAnnotations;

namespace MyVillageApp.Domain.FamilyMembers
{
    public class FamilyMember
    {
        public int Id { get; set; }

        public int FamilyId { get; set; }

        public string Name { get; set; }

        public string AadhaarNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public bool IsPhysicallyDisabled { get; set; }

        public bool IsMarried { get; set; }
    }
}