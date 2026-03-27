namespace MyVillageApp.Models.Home
{
    public class HomeWishModel
    {
        public string WishName { get; set; }
        public string DayName { get; set; }
        public DateTime DateOfCreate { get; set; }
        public List<HomeMediaModel> MediaItems { get; set; } = new();
    }
}