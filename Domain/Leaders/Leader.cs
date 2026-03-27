namespace MyVillageApp.Domain.Leaders
{
    public class Leader
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string VillageRole { get; set; }

        public int? WardNumber { get; set; }

        public string WardName { get; set; }

        public string Photo { get; set; }
    }
}