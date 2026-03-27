namespace MyVillageApp.Models.Admin
{
    public class FamilyMemberListModel
    {
        public int Id { get; set; }

        public int FamilyId { get; set; }

        public string Name { get; set; }

        public string AadhaarNumber { get; set; }

        public string PhoneNumber { get; set; }

        public string Gender { get; set; }

        public string Professions { get; set; }

        public string Schemes { get; set; }

        public string Pensions { get; set; }

        public string VehicleTypes { get; set; }

        public bool IsPhysicallyDisabled { get; set; }

        public bool IsMarried { get; set; }
    }
}