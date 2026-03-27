namespace MyVillageApp.Domain.Wishes
{
    public class WishMedia
    {
        public int Id { get; set; }
        public int WishId { get; set; }
        public string FileName { get; set; }
        public string MediaType { get; set; } // Photo / Video
    }
}