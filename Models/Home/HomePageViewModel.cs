using MyVillageApp.Domain.Announcements;
using MyVillageApp.Domain.Gallery;

namespace MyVillageApp.Models.Home
{
    public class HomePageViewModel
    {
        public List<HomeWishModel> Wishes { get; set; } = new();
        public List<Announcement> Announcements { get; set; } = new();
        public List<HomeCelebrationModel> Celebrations { get; set; } = new();
        public List<VillageGallery> VillageGalleryItems { get; set; } = new();
    }
}