namespace MyVillageApp.Domain.Wishes
{
    public class Wish
    {
        public int Id { get; set; }
        public string WishName { get; set; }
        public string DayName { get; set; }
        public DateTime DateOfCreate { get; set; }
    }
}
