using Microsoft.AspNetCore.Mvc;
using MyVillageApp.Services.Interfaces;
using MyVillageApp.Domain.Users;
using MyVillageApp.Domain.Schemes;
using MyVillageApp.Domain.Professions;
using MyVillageApp.Domain.Leaders;
using Microsoft.AspNetCore.Http.HttpResults;
using MyVillageApp.Domain.Pensions;
using MyVillageApp.Domain.Families;
using MyVillageApp.Domain.VehicleTypes;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyVillageApp.Models.Admin;
using MyVillageApp.Domain.FamilyMembers;
using MyVillageApp.Services.FamilyMembers;
using MyVillageApp.Services.Pets;
using MyVillageApp.Services.Families;
using System.Linq;
using MyVillageApp.Domain.Announcements;
using MyVillageApp.Domain.Celebrations;
namespace MyVillageApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISchemeService _schemeService;
        private readonly IProfessionService _professionService;
        private readonly ILeaderService _leaderService;
        private readonly IWebHostEnvironment _env;
        private readonly IVillageGalleryService _villageGalleryService;
        private readonly IPensionService _pensionService;
        private readonly IFamilyService _familyService;
        private readonly IVehicleTypeService _vehicleTypeService;
        private readonly IFamilyMemberService _familyMemberService;
        private readonly IFamilyMemberMappingService _familyMemberMappingService;
        private readonly IPetService _petService;
        private readonly IFamilyPetService _familyPetService;
        private readonly IWishService _wishService;
        private readonly IWishMediaService _wishMediaService;
        private readonly ICelebrationService _celebrationService;
        private readonly ICelebrationMediaService _celebrationMediaService;
        private readonly IAnnouncementService _announcementService;
      
        public AdminController(IUserService userService, ISchemeService schemeService,IProfessionService professionService,ILeaderService leaderService,
    IWebHostEnvironment env,IVillageGalleryService villageGalleryService,IPensionService pensionService,IFamilyService familyService,IVehicleTypeService vehicleTypeService,IFamilyMemberService familyMemberService ,IFamilyMemberMappingService familyMemberMappingService,IPetService petService,IFamilyPetService familyPetService, IWishService wishService, IWishMediaService wishMediaService, ICelebrationService celebrationService, ICelebrationMediaService celebrationMediaService, IAnnouncementService announcementService)
        {
            _userService = userService;
            _schemeService = schemeService;
            _professionService = professionService;
            _leaderService = leaderService;
            _env = env;
            _villageGalleryService = villageGalleryService;
            _pensionService = pensionService;
            _familyService= familyService;
            _vehicleTypeService = vehicleTypeService;
            _familyMemberService = familyMemberService;
            _familyMemberMappingService = familyMemberMappingService;
            _petService = petService;
            _familyPetService = familyPetService;
            _wishService = wishService;
            _wishMediaService = wishMediaService;
            _wishMediaService= wishMediaService;
            _celebrationService= celebrationService;
            _celebrationMediaService= celebrationMediaService;
            _announcementService= announcementService;
        }

        // =========================
        // USERS LIST
        // =========================

        public async Task<IActionResult> Users(string searchText, int page = 1)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            const int pageSize = 20;

            var users = await _userService.GetAllUsersAsync();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.Trim();
                users = users
                    .Where(x => !string.IsNullOrWhiteSpace(x.UserName) && x.UserName.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var totalCount = users.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (page < 1)
                page = 1;

            if (totalPages > 0 && page > totalPages)
                page = totalPages;

            var pagedUsers = users
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.SearchText = searchText;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalCount = totalCount;

            return View(pagedUsers);
        }

        // =========================
        // UPDATE ROLE
        // =========================

        [HttpPost]
        public async Task<IActionResult> UpdateRole(int userId, string role)
        {
            var user = await _userService.GetUserByIdAsync(userId);

            if (user != null)
            {
                user.Role = role;
                await _userService.UpdateUserAsync(user);
            }

            return RedirectToAction("Users");
        }

        public IActionResult Dashboard()
        {
            return View();
        }
		public async Task<IActionResult> Schemes()
        {
            var schemes = await _schemeService.GetAllSchemesAsync();
            if (schemes == null)
                return View();

            return View(schemes ?? new List<Scheme>());
		}
		public IActionResult CreateScheme()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateScheme(string schemeName)
        {
            var scheme = new Scheme
            {
                SchemeName = schemeName
            };

            await _schemeService.InsertSchemeAsync(scheme);

            return RedirectToAction("Schemes");
        }
        public async Task<IActionResult> EditScheme(int id)
        {
            var scheme = await _schemeService.GetSchemeByIdAsync(id);

            return View(scheme);
        }

        [HttpPost]
        public async Task<IActionResult> EditScheme(Scheme scheme)
        {
            await _schemeService.UpdateSchemeAsync(scheme);

            return RedirectToAction("Schemes");
        }
		public async Task<IActionResult> DeleteScheme(int id)
		{
			var scheme = await _schemeService.GetSchemeByIdAsync(id);

			if (scheme == null)
				return RedirectToAction("Schemes");

			return View(scheme);  
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteSchemeConfirmed(int id)
		{
			var scheme = await _schemeService.GetSchemeByIdAsync(id);

			if (scheme != null)
				await _schemeService.DeleteSchemeAsync(scheme);

			return RedirectToAction("Schemes");
		}
        public async Task<IActionResult> Professions()
        {
            var professions = await _professionService.GetAllProfessionsAsync();

            return View(professions);
        }
        public IActionResult CreateProfession()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfession(string professionName)
        {
            var profession = new Profession
            {
                ProfessionName = professionName
            };
           // var alreadyprofession=await _professionService.GetProfeesionByNameAsync(professionName.Trim());
            
            await _professionService.InsertProfessionAsync(profession);

            return RedirectToAction("Professions");
        }
      
        public async Task<IActionResult> EditProfession(int id)
        {
            var profession = await _professionService.GetProfessionByIdAsync(id);

            return View(profession);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfession(Profession profession)
        {
            await _professionService.UpdateProfessionAsync(profession);

            return RedirectToAction("Professions");
        }

        public async Task<IActionResult> DeleteProfession(int id)
        {
            var profession = await _professionService.GetProfessionByIdAsync(id);

            if (profession == null)
                return RedirectToAction("Professions");

            return View(profession);

           
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteProfessionConfirmed(int id)
		{
			var profession = await _professionService.GetProfessionByIdAsync(id);

            if (profession!= null)
                await _professionService.DeleteProfessionAsync(profession);

            return RedirectToAction("Professions");

        }
        public async Task<IActionResult> Leaders()
        {
            var leaders = await _leaderService.GetAllLeadersAsync();
            return View(leaders);
        }
        public IActionResult CreateLeader()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeader(Leader leader, IFormFile photoFile)
        {
            if (photoFile != null && photoFile.Length > 0)
            {
                string folderPath = Path.Combine(_env.WebRootPath, "leaderphotos");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                string path = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photoFile.CopyToAsync(stream);
                }

                leader.Photo = fileName;
            }
            if(leader.WardName==null)
                leader.WardName="_";
            if (leader.WardNumber == null)
                leader.WardNumber =0;
            await _leaderService.InsertLeaderAsync(leader);

            return RedirectToAction("Leaders");
        }

        public async Task<IActionResult> EditLeader(int id)
        {
            var leader = await _leaderService.GetLeaderByIdAsync(id);

            if (leader == null)
                return RedirectToAction("Leaders");

            return View(leader);
        }

        [HttpPost]
        public async Task<IActionResult> EditLeader(Leader leader, IFormFile photoFile)
        {
            var existingLeader = await _leaderService.GetLeaderByIdAsync(leader.Id);

            if (existingLeader == null)
                return RedirectToAction("Leaders");

            existingLeader.Name = leader.Name;
            existingLeader.VillageRole = leader.VillageRole;
            existingLeader.WardNumber = leader.WardNumber;
            existingLeader.WardName = leader.WardName;

            if (photoFile != null && photoFile.Length > 0)
            {
                string folderPath = Path.Combine(_env.WebRootPath, "leaderphotos");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                string path = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photoFile.CopyToAsync(stream);
                }

                existingLeader.Photo = fileName;
            }

            await _leaderService.UpdateLeaderAsync(existingLeader);

            return RedirectToAction("Leaders");
        }
        public async Task<IActionResult> DeleteLeader(int id)
        {
            var leader = await _leaderService.GetLeaderByIdAsync(id);

            if (leader == null)
                return RedirectToAction("Leaders");

            return View(leader);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLeaderConfirmed(int id)
        {
            var leader = await _leaderService.GetLeaderByIdAsync(id);

            if (leader != null)
                await _leaderService.DeleteLeaderAsync(leader);

            return RedirectToAction("Leaders");

        }
        public async Task<IActionResult> VillageGallery()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var list = await _villageGalleryService.GetAllAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult CreateVillageGallery()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVillageGallery(string title, string mediaType, string description, IFormFile file)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            if (file != null && file.Length > 0)
            {
                string folderPath = Path.Combine(_env.WebRootPath, "villagegallery");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string fullPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var entity = new MyVillageApp.Domain.Gallery.VillageGallery
                {
                    Title = title,
                    MediaType = mediaType,
                    Description = description,
                    FileName = fileName
                };

                await _villageGalleryService.InsertAsync(entity);
            }

            return RedirectToAction("VillageGallery");
        }
        public async Task<IActionResult> EditVillageGallery(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var entity = await _villageGalleryService.GetByIdAsync(id);

            if (entity == null)
                return RedirectToAction("VillageGallery");

            return View(entity);
        }

        [HttpPost]
        [RequestSizeLimit(824288000)]
        public async Task<IActionResult> EditVillageGallery(MyVillageApp.Domain.Gallery.VillageGallery model, IFormFile file)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var entity = await _villageGalleryService.GetByIdAsync(model.Id);

            if (entity == null)
                return RedirectToAction("VillageGallery");

            entity.Title = model.Title;
            entity.MediaType = model.MediaType;
            entity.Description = model.Description;

            if (file != null && file.Length > 0)
            {
                string folderPath = Path.Combine(_env.WebRootPath, "villagegallery");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string fullPath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                entity.FileName = fileName;
            }

            await _villageGalleryService.UpdateAsync(entity);

            return RedirectToAction("VillageGallery");
        }

        public async Task<IActionResult> DeleteVillageGallery(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var gallery = await _villageGalleryService.GetByIdAsync(id);

            if (gallery == null)
                return RedirectToAction("VillageGallery");

            return View(gallery);
        }
        public async Task<IActionResult> DeleteVillageGalleryConfirmed(int id)
        {
            var gallery= await _villageGalleryService.GetByIdAsync(id);

            if (gallery != null)
                await _villageGalleryService.DeleteAsync(gallery);

            return RedirectToAction("VillageGallery");

        }
        public async Task<IActionResult> Pensions()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var list = await _pensionService.GetAllAsync();

            return View(list);
        }
        public IActionResult CreatePension()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePension(Pension model)
        {
            await _pensionService.InsertAsync(model);

            return RedirectToAction("Pensions");
        }
        public async Task<IActionResult> EditPension(int id)
        {
            var entity = await _pensionService.GetByIdAsync(id);

            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> EditPension(Pension model)
        {
            await _pensionService.UpdateAsync(model);

            return RedirectToAction("Pensions");
        }
        public async Task<IActionResult> DeletePension(int id)
        {

			var pension = await _pensionService.GetByIdAsync(id);

			if (pension == null)
				return RedirectToAction("Pensions");

			return View(pension);
		}

		public async Task<IActionResult> DeletePensionConfirmed(int id)
		{

			var pension= await _pensionService.GetByIdAsync(id);

			if (pension != null)
				await _pensionService.DeleteAsync(pension);

			return RedirectToAction("Pensions");
        }
        public async Task<IActionResult> Families(string searchText, int page = 1)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            const int pageSize = 20;

            ViewBag.SearchText = searchText;

            var families = string.IsNullOrWhiteSpace(searchText)
                ? await _familyService.GetAllAsync()
                : await _familyService.SearchFamiliesAsync(searchText);

            var allPets = await _petService.GetAllAsync();

            var allFamilyModels = new List<FamilyListModel>();

            foreach (var item in families)
            {
                var petMaps = await _familyPetService.GetByFamilyIdAsync(item.Id);

                var petDisplay = "-";

                if (petMaps.Any())
                {
                    var petTexts = petMaps.Select(x =>
                    {
                        var petName = allPets.FirstOrDefault(p => p.Id == x.PetId)?.PetName ?? "Unknown";
                        return $"{petName} ({x.PetCount})";
                    }).ToList();

                    petDisplay = string.Join(", ", petTexts);
                }

                allFamilyModels.Add(new FamilyListModel
                {
                    Id = item.Id,
                    OwnerName = item.OwnerName,
                    PhoneNumber = item.PhoneNumber,
                    RationCardNumber = item.RationCardNumber,
                    TotalAcres = item.TotalAcres,
                    VehiclesCount = item.VehiclesCount,
                    OwnHouse = item.OwnHouse,
                    GasConnection = item.GasConnection,
                    WaterConnection = item.WaterConnection,
                    PetsDisplay = petDisplay
                });
            }

            var totalCount = allFamilyModels.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (page < 1)
                page = 1;

            if (totalPages > 0 && page > totalPages)
                page = totalPages;

            var pagedFamilies = allFamilyModels
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.TotalCount = totalCount;

            return View(pagedFamilies);
        }
        public async Task<IActionResult> CreateFamily()
        {
            var model = new FamilyModel();
            await PrepareFamilyModelAsync(model);
            return View(model);
        }

        private async Task PrepareFamilyModelAsync(FamilyModel model)
        {
            var pets = await _petService.GetAllAsync();

            model.AvailablePets = pets.Select(x => new SelectListItem
            {
                Text = x.PetName,
                Value = x.Id.ToString()
            }).ToList();

            if (model.Pets == null || !model.Pets.Any())
            {
                model.Pets = new List<FamilyPetInputModel>
        {
            new FamilyPetInputModel()
        };
            }
        }


        
        [HttpPost]
        public async Task<IActionResult> CreateFamily(FamilyModel model)
        {
            if (!ModelState.IsValid)
            {
                await PrepareFamilyModelAsync(model);
                return View(model);
            }

            if (!string.IsNullOrWhiteSpace(model.RationCardNumber))
            {
                var alreadyFamily = await _familyService.GetFamilyByRationCardNumberAsync(model.RationCardNumber);

                if (alreadyFamily != null)
                {
                    ModelState.AddModelError("RationCardNumber", "Ration card number already exists.");
                    await PrepareFamilyModelAsync(model);
                    return View(model);
                }
            }

            var entity = new Family
            {
                OwnerName = model.OwnerName,
                PhoneNumber = model.PhoneNumber,
                RationCardNumber = model.RationCardNumber,
                TotalAcres =Convert.ToDecimal( model.TotalAcres),
                VehiclesCount =Convert.ToInt16( model.VehiclesCount),
                OwnHouse = model.OwnHouse,
                GasConnection = model.GasConnection,
                WaterConnection = model.WaterConnection
            };

            await _familyService.InsertAsync(entity);

            var petMaps = (model.Pets ?? new List<FamilyPetInputModel>())
       .Where(x => x.PetId > 0 && x.PetCount > 0)
       .Select(x => new FamilyPetMap
       {
           PetId = x.PetId,
           PetCount = x.PetCount
       })
       .ToList();
            await _familyPetService.SaveFamilyPetsAsync(entity.Id, petMaps);

            return RedirectToAction("Families");
        }
        public async Task<IActionResult> EditFamily(int id)
        {
            var entity = await _familyService.GetByIdAsync(id);

            if (entity == null)
                return RedirectToAction("Families");

            var petMaps = await _familyPetService.GetByFamilyIdAsync(id);

            var model = new FamilyModel
            {
                Id = entity.Id,
                OwnerName = entity.OwnerName,
                PhoneNumber = entity.PhoneNumber,
                RationCardNumber = entity.RationCardNumber,
                TotalAcres = entity.TotalAcres,
                VehiclesCount = entity.VehiclesCount,
                OwnHouse = entity.OwnHouse,
                GasConnection = entity.GasConnection,
                WaterConnection = entity.WaterConnection,
                Pets = petMaps.Select(x => new FamilyPetInputModel
                {
                    PetId = x.PetId,
                    PetCount = x.PetCount
                }).ToList()
            };

            await PrepareFamilyModelAsync(model);

            return View(model);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> EditFamily(FamilyModel model)
        {
            if (!ModelState.IsValid)
            {
                await PrepareFamilyModelAsync(model);
                return View(model);
            }

            var entity = await _familyService.GetByIdAsync(model.Id);

            if (entity == null)
                return RedirectToAction("Families");

            entity.OwnerName = model.OwnerName;
            entity.PhoneNumber = model.PhoneNumber;
            entity.RationCardNumber = model.RationCardNumber;
            entity.TotalAcres =Convert.ToDecimal( model.TotalAcres);
            entity.VehiclesCount =Convert.ToInt32 (model.VehiclesCount);
            entity.OwnHouse = model.OwnHouse;
            entity.GasConnection = model.GasConnection;
            entity.WaterConnection = model.WaterConnection;

            await _familyService.UpdateAsync(entity);

            var petMaps = (model.Pets ?? new List<FamilyPetInputModel>())
                .Where(x => x.PetId > 0 && x.PetCount > 0)
                .Select(x => new FamilyPetMap
                {
                    PetId = x.PetId,
                    PetCount = x.PetCount
                })
                .ToList();

            await _familyPetService.SaveFamilyPetsAsync(entity.Id, petMaps);

            return RedirectToAction("Families");
        }
        public async Task<IActionResult> DeleteFamily(int id)
		{

			var family = await _familyService.GetByIdAsync(id);

			if (family == null)
				return RedirectToAction("Families");

			return View(family);
		}

		public async Task<IActionResult> DeleteFamilyConfirmed(int id)
		{

			var family = await _familyService.GetByIdAsync(id);

			if (family != null)
				await _familyService.DeleteAsync(family);

			return RedirectToAction("Families");
		}
        public async Task<IActionResult> VehicleTypes()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var list = await _vehicleTypeService.GetAllAsync();

            return View(list);
        }

        public IActionResult CreateVehicleType()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicleType(VehicleType model)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");
            if (!ModelState.IsValid)
                return View(model);

            if (!string.IsNullOrWhiteSpace(model.VehicleTypeName))
            {
                var alredyvechile = await _vehicleTypeService.GetVechileByTypeAsync(model.VehicleTypeName);

                if (alredyvechile != null)
                {
                    ModelState.AddModelError("VehicleTypeName", "Vechile name already exists.");
                    return View(model);
                }
            }

            await _vehicleTypeService.InsertAsync(model);

            return RedirectToAction("VehicleTypes");
        }

        public async Task<IActionResult> EditVehicleType(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var entity = await _vehicleTypeService.GetByIdAsync(id);

            return View(entity);
        }

        [HttpPost]
        public async Task<IActionResult> EditVehicleType(VehicleType model)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            await _vehicleTypeService.UpdateAsync(model);

            return RedirectToAction("VehicleTypes");
        }

       
        public async Task<IActionResult> DeleteVehicleType(int id)
        {

            var vechiletype= await _vehicleTypeService.GetByIdAsync(id);

            if (vechiletype == null)
                return RedirectToAction("VehicleTypes");

            return View(vechiletype);
        }

        public async Task<IActionResult> DeleteVehicleTypeConfirmed(int id)
        {
            var vechiletype = await _vehicleTypeService.GetByIdAsync(id);

            if (vechiletype != null)
                await _vehicleTypeService.DeleteAsync(vechiletype);

            return RedirectToAction("VehicleTypes");
        }
        private async Task PrepareFamilyMemberModelAsync(FamilyMemberModel model)
        {
            var professions = await _professionService.GetAllProfessionsAsync();
            var schemes = await _schemeService.GetAllSchemesAsync();
            var vehicleTypes = await _vehicleTypeService.GetAllAsync();
            var pensions = await _pensionService.GetAllAsync();

            model.AvailableProfessions = professions
                .Select(x => new SelectListItem
                {
                    Text = x.ProfessionName,
                    Value = x.Id.ToString()
                }).ToList();

            model.AvailableSchemes = schemes
                .Select(x => new SelectListItem
                {
                    Text = x.SchemeName,
                    Value = x.Id.ToString()
                }).ToList();

            model.AvailableVehicleTypes = vehicleTypes
                .Select(x => new SelectListItem
                {
                    Text = x.VehicleTypeName,
                    Value = x.Id.ToString()
                }).ToList();

            model.AvailablePensions = pensions
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
        }
        public async Task<IActionResult> FamilyMembers(int familyId, string searchText)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var family = await _familyService.GetByIdAsync(familyId);
            if (family == null)
                return RedirectToAction("Families");

            ViewBag.FamilyId = familyId;
            ViewBag.OwnerName = family.OwnerName;
            ViewBag.SearchText = searchText;

            var members = string.IsNullOrWhiteSpace(searchText)
                ? await _familyMemberService.GetByFamilyIdAsync(familyId)
                : await _familyMemberService.SearchFamilyMembersAsync(familyId, searchText);

            var model = new List<FamilyMemberListModel>();

            foreach (var item in members)
            {
                var professions = await _familyMemberMappingService.GetProfessionNamesAsync(item.Id);
                var schemes = await _familyMemberMappingService.GetSchemeNamesAsync(item.Id);
                var pensions = await _familyMemberMappingService.GetPensionNamesAsync(item.Id);
                var vehicleTypes = await _familyMemberMappingService.GetVehicleTypeNamesAsync(item.Id);

                model.Add(new FamilyMemberListModel
                {
                    Id = item.Id,
                    FamilyId = item.FamilyId,
                    Name = item.Name,
                    AadhaarNumber = item.AadhaarNumber,
                    PhoneNumber = item.PhoneNumber,
                    Gender = item.Gender,
                    Professions = professions.Any() ? string.Join(", ", professions) : "-",
                    Schemes = schemes.Any() ? string.Join(", ", schemes) : "-",
                    Pensions = pensions.Any() ? string.Join(", ", pensions) : "-",
                    VehicleTypes = vehicleTypes.Any() ? string.Join(", ", vehicleTypes) : "-",
                    IsPhysicallyDisabled = item.IsPhysicallyDisabled,
                    IsMarried = item.IsMarried
                });
            }

            return View(model);
        }
        public async Task<IActionResult> CreateFamilyMember(int familyId)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var model = new FamilyMemberModel
            {
                FamilyId = familyId
            };

            await PrepareFamilyMemberModelAsync(model);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateFamilyMember(FamilyMemberModel model)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            

            var entity = new FamilyMember
            {
                FamilyId = model.FamilyId,
                Name = model.Name,
                AadhaarNumber = model.AadhaarNumber,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                IsPhysicallyDisabled = model.IsPhysicallyDisabled,
                IsMarried = model.IsMarried
            };
           
            if (!string.IsNullOrWhiteSpace(entity.AadhaarNumber))
            {
                var alredyFamilyMember = await _familyMemberService.GetFamilyMemberByAadhaarNumberAsync(entity.AadhaarNumber);

                if (alredyFamilyMember != null)
                {
                    ModelState.AddModelError("AadhaarNumber", "Alreay Other Person These AdharNumber");
                    return View(model);
                }
            }
            await _familyMemberService.InsertAsync(entity);
            await _familyMemberMappingService.SaveProfessionsAsync(entity.Id, model.SelectedProfessionIds);
            await _familyMemberMappingService.SaveSchemesAsync(entity.Id, model.SelectedSchemeIds);
            await _familyMemberMappingService.SavePensionsAsync(entity.Id, model.SelectedPensionIds);
            await _familyMemberMappingService.SaveVehicleTypesAsync(entity.Id, model.SelectedVehicleTypeIds);

            return RedirectToAction("FamilyMembers", new { familyId = model.FamilyId });

            
        }
        public async Task<IActionResult> EditFamilyMember(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
                return RedirectToAction("Index", "Home");
            var entity = await _familyMemberService.GetByIdAsync(id);

            if (entity == null)
                return RedirectToAction("Families");

            var model = new FamilyMemberModel
            {
                Id = entity.Id,
                FamilyId = entity.FamilyId,
                Name = entity.Name,
                AadhaarNumber = entity.AadhaarNumber,
                PhoneNumber = entity.PhoneNumber,
                Gender = entity.Gender,
                IsPhysicallyDisabled = entity.IsPhysicallyDisabled,
                IsMarried = entity.IsMarried,
                SelectedProfessionIds = await _familyMemberMappingService.GetProfessionIdsAsync(entity.Id),
                SelectedSchemeIds = await _familyMemberMappingService.GetSchemeIdsAsync(entity.Id),
                SelectedPensionIds = await _familyMemberMappingService.GetPensionIdsAsync(entity.Id),
                SelectedVehicleTypeIds = await _familyMemberMappingService.GetVehicleTypeIdsAsync(entity.Id)
            };

            await PrepareFamilyMemberModelAsync(model);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditFamilyMember(FamilyMemberModel model)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
                return RedirectToAction("Index", "Home");


            var entity = await _familyMemberService.GetByIdAsync(model.Id);

            if (entity == null)
                return RedirectToAction("Families");

            entity.Name = model.Name;
            entity.AadhaarNumber = model.AadhaarNumber;
            entity.PhoneNumber = model.PhoneNumber;
            entity.Gender = model.Gender;
            entity.IsPhysicallyDisabled = model.IsPhysicallyDisabled;
            entity.IsMarried = model.IsMarried;

            await _familyMemberService.UpdateAsync(entity);

            await _familyMemberMappingService.SaveProfessionsAsync(entity.Id, model.SelectedProfessionIds);
            await _familyMemberMappingService.SaveSchemesAsync(entity.Id, model.SelectedSchemeIds);
            await _familyMemberMappingService.SavePensionsAsync(entity.Id, model.SelectedPensionIds);
            await _familyMemberMappingService.SaveVehicleTypesAsync(entity.Id, model.SelectedVehicleTypeIds);

            return RedirectToAction("FamilyMembers", new { familyId = model.FamilyId });
        }
        public async Task<IActionResult> DeleteFamilyMember(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var familymember = await _familyMemberService.GetByIdAsync(id);

            if (familymember == null)
                return RedirectToAction("FamilyMembers");

            return View(familymember);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFamilyMemberConfirmed(int id)
        {
            var familymember = await _familyMemberService.GetByIdAsync(id);

            if (familymember == null)
                return RedirectToAction("Families");

            int familyId = familymember.FamilyId;

            await _familyMemberMappingService.DeleteAllMappingsAsync(id);
            await _familyMemberService.DeleteAsync(familymember);

            return RedirectToAction("FamilyMembers", new { familyId = familyId });
        }
        public async Task<IActionResult> VillagePeople(
      string searchText,
      string ownerName,
      int? professionId,
      int? schemeId,
      int? pensionId,
      int? vehicleTypeId,
      bool male = false,
      bool female = false,
      bool? isDisabled = null,
      bool? isMarried = null,
      int page = 1)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin" && role != "DataEntryOperator")
                return RedirectToAction("Index", "Home");

            const int pageSize = 20;

            var professionsMaster = await _professionService.GetAllProfessionsAsync();
            var schemesMaster = await _schemeService.GetAllSchemesAsync();
            var pensionsMaster = await _pensionService.GetAllAsync();
            var vehicleTypesMaster = await _vehicleTypeService.GetAllAsync();

            var filterModel = new VillagePeopleFilterModel
            {
                SearchText = searchText,
                OwnerName = ownerName,
                ProfessionId = professionId,
                SchemeId = schemeId,
                PensionId = pensionId,
                VehicleTypeId = vehicleTypeId,
                Male = male,
                Female = female,
                IsDisabled = isDisabled,
                IsMarried = isMarried,
                AvailableProfessions = professionsMaster.Select(x => new SelectListItem
                {
                    Text = x.ProfessionName,
                    Value = x.Id.ToString()
                }).ToList(),
                AvailableSchemes = schemesMaster.Select(x => new SelectListItem
                {
                    Text = x.SchemeName,
                    Value = x.Id.ToString()
                }).ToList(),
                AvailablePensions = pensionsMaster.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList(),
                AvailableVehicleTypes = vehicleTypesMaster.Select(x => new SelectListItem
                {
                    Text = x.VehicleTypeName,
                    Value = x.Id.ToString()
                }).ToList()
            };

            var people = await _familyMemberService.GetAllAsync();
            var model = new List<VillagePeopleListModel>();

            foreach (var item in people)
            {
                var family = await _familyService.GetByIdAsync(item.FamilyId);

                var professionIds = await _familyMemberMappingService.GetProfessionIdsAsync(item.Id);
                var schemeIds = await _familyMemberMappingService.GetSchemeIdsAsync(item.Id);
                var pensionIds = await _familyMemberMappingService.GetPensionIdsAsync(item.Id);
                var vehicleTypeIds = await _familyMemberMappingService.GetVehicleTypeIdsAsync(item.Id);

                var professions = await _familyMemberMappingService.GetProfessionNamesAsync(item.Id);
                var schemes = await _familyMemberMappingService.GetSchemeNamesAsync(item.Id);
                var pensions = await _familyMemberMappingService.GetPensionNamesAsync(item.Id);
                var vehicleTypes = await _familyMemberMappingService.GetVehicleTypeNamesAsync(item.Id);

                model.Add(new VillagePeopleListModel
                {
                    Id = item.Id,
                    FamilyId = item.FamilyId,
                    FamilyOwnerName = family != null ? family.OwnerName : "-",
                    Name = item.Name,
                    AadhaarNumber = item.AadhaarNumber,
                    PhoneNumber = item.PhoneNumber,
                    Gender = item.Gender,
                    Professions = professions.Any() ? string.Join(", ", professions) : "-",
                    Schemes = schemes.Any() ? string.Join(", ", schemes) : "-",
                    Pensions = pensions.Any() ? string.Join(", ", pensions) : "-",
                    VehicleTypes = vehicleTypes.Any() ? string.Join(", ", vehicleTypes) : "-",
                    IsPhysicallyDisabled = item.IsPhysicallyDisabled,
                    IsMarried = item.IsMarried,
                    ProfessionIds = professionIds,
                    SchemeIds = schemeIds,
                    PensionIds = pensionIds,
                    VehicleTypeIds = vehicleTypeIds
                });
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.Trim();

                model = model
                    .Where(x => !string.IsNullOrWhiteSpace(x.Name) &&
                                x.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(ownerName))
            {
                ownerName = ownerName.Trim();

                model = model
                    .Where(x => !string.IsNullOrWhiteSpace(x.FamilyOwnerName) &&
                                x.FamilyOwnerName.Contains(ownerName, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (professionId.HasValue && professionId.Value > 0)
            {
                model = model
                    .Where(x => x.ProfessionIds != null && x.ProfessionIds.Contains(professionId.Value))
                    .ToList();
            }

            if (schemeId.HasValue && schemeId.Value > 0)
            {
                model = model
                    .Where(x => x.SchemeIds != null && x.SchemeIds.Contains(schemeId.Value))
                    .ToList();
            }

            if (pensionId.HasValue && pensionId.Value > 0)
            {
                model = model
                    .Where(x => x.PensionIds != null && x.PensionIds.Contains(pensionId.Value))
                    .ToList();
            }

            if (vehicleTypeId.HasValue && vehicleTypeId.Value > 0)
            {
                model = model
                    .Where(x => x.VehicleTypeIds != null && x.VehicleTypeIds.Contains(vehicleTypeId.Value))
                    .ToList();
            }

            if (male && !female)
            {
                model = model
                    .Where(x => !string.IsNullOrWhiteSpace(x.Gender) &&
                                x.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else if (!male && female)
            {
                model = model
                    .Where(x => !string.IsNullOrWhiteSpace(x.Gender) &&
                                x.Gender.Equals("Female", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else if (male && female)
            {
                model = model
                    .Where(x => !string.IsNullOrWhiteSpace(x.Gender) &&
                               (x.Gender.Equals("Male", StringComparison.OrdinalIgnoreCase) ||
                                x.Gender.Equals("Female", StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            if (isDisabled.HasValue)
            {
                model = model
                    .Where(x => x.IsPhysicallyDisabled == isDisabled.Value)
                    .ToList();
            }

            if (isMarried.HasValue)
            {
                model = model
                    .Where(x => x.IsMarried == isMarried.Value)
                    .ToList();
            }

            var totalCount = model.Count;
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (page < 1)
                page = 1;

            if (totalPages > 0 && page > totalPages)
                page = totalPages;

            filterModel.CurrentPage = page;
            filterModel.TotalPages = totalPages;
            filterModel.TotalCount = totalCount;
            filterModel.Results = model
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View(filterModel);
        }
        public async Task<IActionResult> Pets()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var pets = await _petService.GetAllAsync();
            return View(pets);
        }
        public IActionResult CreatePet()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            return View(new MyVillageApp.Domain.Pets.Pet());
        }
        [HttpPost]
        public async Task<IActionResult> CreatePet(MyVillageApp.Domain.Pets.Pet model)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            if (string.IsNullOrWhiteSpace(model.PetName))
            {
                ModelState.AddModelError("PetName", "Pet name is required.");
                return View(model);
            }

            await _petService.InsertAsync(model);

            return RedirectToAction("Pets");
        }
        public async Task<IActionResult> EditPet(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var pet = await _petService.GetByIdAsync(id);

            if (pet == null)
                return RedirectToAction("Pets");

            return View(pet);
        }
        [HttpPost]
        public async Task<IActionResult> EditPet(MyVillageApp.Domain.Pets.Pet model)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var pet = await _petService.GetByIdAsync(model.Id);

            if (pet == null)
                return RedirectToAction("Pets");

            if (string.IsNullOrWhiteSpace(model.PetName))
            {
                ModelState.AddModelError("PetName", "Pet name is required.");
                return View(model);
            }

            pet.PetName = model.PetName;

            await _petService.UpdateAsync(pet);

            return RedirectToAction("Pets");
        }

        public async Task<IActionResult> DeletePet(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var pet = await _petService.GetByIdAsync(id);

            if (pet == null)
                return RedirectToAction("Pets");

            return View(pet);
        }
       
        [HttpPost]
         [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePetConfirmed(int id)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var pet = await _petService.GetByIdAsync(id);

            if (pet != null)
                await _petService.DeleteAsync(pet);

            return RedirectToAction("Pets");
        }

        public async Task<IActionResult> Wishes()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var wishes = await _wishService.GetAllAsync();
            if (wishes==null)
                return RedirectToAction("Wishes");
            return View(wishes);
        }
     
public IActionResult CreateWish()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            return View();
        }
      
        [HttpPost]
        [RequestSizeLimit(824288000)]
public async Task<IActionResult> CreateWish(string wishName, string dayName, DateTime dateOfCreate, List<IFormFile> files)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var wish = new MyVillageApp.Domain.Wishes.Wish
            {
                WishName = wishName,
                DayName = dayName,
                DateOfCreate = dateOfCreate
            };

            await _wishService.InsertAsync(wish);

            if (files != null && files.Any())
            {
                string folderPath = Path.Combine(_env.WebRootPath, "wishmedia");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        string fullPath = Path.Combine(folderPath, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var media = new MyVillageApp.Domain.Wishes.WishMedia
                        {
                            WishId = wish.Id,
                            FileName = fileName,
                            MediaType = file.ContentType.StartsWith("video") ? "Video" : "Photo"
                        };

                        await _wishMediaService.InsertAsync(media);
                    }
                }
            }

            return RedirectToAction("Wishes");
        }
       
public async Task<IActionResult> Celebrations()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var celebrations = await _celebrationService.GetAllAsync();
            return View(celebrations);
        }
     
public IActionResult CreateCelebration()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            return View();
        }
       
        [HttpPost]
        [RequestSizeLimit(824288000)]
public async Task<IActionResult> CreateCelebration(string celebrationName, List<IFormFile> files)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var celebration = new MyVillageApp.Domain.Celebrations.Celebration
            {
                CelebrationName = celebrationName
            };

            await _celebrationService.InsertAsync(celebration);

            if (files != null && files.Any())
            {
                string folderPath = Path.Combine(_env.WebRootPath, "celebrationmedia");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                        string fullPath = Path.Combine(folderPath, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var media = new MyVillageApp.Domain.Celebrations.CelebrationMedia
                        {
                            CelebrationId = celebration.Id,
                            FileName = fileName,
                            MediaType = file.ContentType.StartsWith("video") ? "Video" : "Photo"
                        };

                        await _celebrationMediaService.InsertAsync(media);
                    }
                }
            }

            return RedirectToAction("Celebrations");
        }
     
public async Task<IActionResult> Announcements()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var announcements = await _announcementService.GetAllAsync();
            return View(announcements);
        }
     
public IActionResult CreateAnnouncement()
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            return View();
        }
       
        [HttpPost]
public async Task<IActionResult> CreateAnnouncement(string message)
        {
            var role = HttpContext.Session.GetString("UserRole");

            if (role != "Admin")
                return RedirectToAction("Index", "Home");

            var entity = new MyVillageApp.Domain.Announcements.Announcement
            {
                Message = message,
                CreatedOn = DateTime.Now
            };

            await _announcementService.InsertAsync(entity);

            return RedirectToAction("Announcements");
        }
    }
}