namespace MyVillageApp.Models.Home
{
    public class HomeCelebrationModel
    {
        public string CelebrationName { get; set; }
        public List<HomeMediaModel> MediaItems { get; set; } = new();
    }
}