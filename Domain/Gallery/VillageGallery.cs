namespace MyVillageApp.Domain.Gallery
{
    public class VillageGallery
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string MediaType { get; set; }

        public string FileName { get; set; }

        public string? Description { get; set; }
    }
}