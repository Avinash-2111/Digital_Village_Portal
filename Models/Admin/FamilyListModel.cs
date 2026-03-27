namespace MyVillageApp.Models.Admin
{
    public class FamilyListModel
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string PhoneNumber { get; set; }
        public string RationCardNumber { get; set; }
        public decimal? TotalAcres { get; set; }
        public int? VehiclesCount { get; set; }
        public bool OwnHouse { get; set; }
        public bool GasConnection { get; set; }
        public bool WaterConnection { get; set; }
        public string PetsDisplay { get; set; }
    }
}