using Microsoft.AspNetCore.Mvc;
using MyVillageApp.Services.Interfaces;
using System.Diagnostics;
using VillagePoratl.Models;

namespace VillagePoratl.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWishService _wishService;
        private readonly IWishMediaService _wishMediaService;
        private readonly ICelebrationService _celebrationService;
        private readonly ICelebrationMediaService _celebrationMediaService;
        private readonly IAnnouncementService _announcementService;
        private readonly IVillageGalleryService _villageGalleryService;
        public HomeController(ILogger<HomeController> logger, IWishService wishService, IWishMediaService wishMediaService, ICelebrationService celebrationService, ICelebrationMediaService celebrationMediaService, IAnnouncementService announcementService, IVillageGalleryService villageGalleryService)
        {
            _logger = logger;
            _wishService = wishService;
            _wishMediaService = wishMediaService;
            _celebrationService = celebrationService;
            _celebrationMediaService = celebrationMediaService;
            _announcementService = announcementService;
            _villageGalleryService = villageGalleryService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new MyVillageApp.Models.Home.HomePageViewModel();

            var wishes = await _wishService.GetAllAsync();
            foreach (var wish in wishes)
            {
                var medias = await _wishMediaService.GetByWishIdAsync(wish.Id);

                model.Wishes.Add(new MyVillageApp.Models.Home.HomeWishModel
                {
                    WishName = wish.WishName,
                    DayName = wish.DayName,
                    DateOfCreate = wish.DateOfCreate,
                    MediaItems = medias.Select(x => new MyVillageApp.Models.Home.HomeMediaModel
                    {
                        FileName = x.FileName,
                        MediaType = x.MediaType,
                        FolderName = "wishmedia"
                    }).ToList()
                });
            }

            model.Announcements = await _announcementService.GetAllAsync();

            var celebrations = await _celebrationService.GetAllAsync();
            foreach (var c in celebrations)
            {
                var medias = await _celebrationMediaService.GetByCelebrationIdAsync(c.Id);

                model.Celebrations.Add(new MyVillageApp.Models.Home.HomeCelebrationModel
                {
                    CelebrationName = c.CelebrationName,
                    MediaItems = medias.Select(x => new MyVillageApp.Models.Home.HomeMediaModel
                    {
                        FileName = x.FileName,
                        MediaType = x.MediaType,
                        FolderName = "celebrationmedia"
                    }).ToList()
                });
            }

            model.VillageGalleryItems = await _villageGalleryService.GetAllAsync();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
