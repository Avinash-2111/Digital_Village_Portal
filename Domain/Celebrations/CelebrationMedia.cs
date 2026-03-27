namespace MyVillageApp.Domain.Celebrations
{
    public class CelebrationMedia
    {
        public int Id { get; set; }
        public int CelebrationId { get; set; }
        public string FileName { get; set; }
        public string MediaType { get; set; } // Photo / Video
    }
}