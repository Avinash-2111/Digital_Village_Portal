namespace MyVillageApp.Domain.Announcements
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}