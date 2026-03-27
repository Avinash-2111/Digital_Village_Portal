using Microsoft.EntityFrameworkCore;
using MyVillageApp.Domain.Announcements;
using MyVillageApp.Domain.Celebrations;
using MyVillageApp.Domain.Families;
using MyVillageApp.Domain.FamilyMembers;
using MyVillageApp.Domain.Gallery;
using MyVillageApp.Domain.Leaders;
using MyVillageApp.Domain.Pensions;
using MyVillageApp.Domain.Pets;
using MyVillageApp.Domain.Professions;
using MyVillageApp.Domain.Schemes;
using MyVillageApp.Domain.Users;
using MyVillageApp.Domain.VehicleTypes;
using MyVillageApp.Domain.Wishes;


namespace MyVillageApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Scheme> Schemes { get; set; }
        public DbSet<Profession> Professions { get; set; }
        public DbSet<Leader> Leaders { get; set; }
        public DbSet<VillageGallery> VillageGallery { get; set; }
        public DbSet<Pension> pensions { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<VehicleType> vehicleTypes { get; set; }
        public DbSet<FamilyMember> FamilyMembers { get; set; }
     
        public DbSet<FamilyMemberProfessionMap> FamilyMemberProfessionMap { get; set; }
        public DbSet<FamilyMemberSchemeMap> FamilyMemberSchemeMap { get; set; }
        public DbSet<FamilyMemberPensionMap> FamilyMemberPensionMap { get; set; }
        public DbSet<FamilyMemberVehicleTypeMap> FamilyMemberVehicleTypeMap { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<FamilyPetMap> FamilyPetMaps { get; set; }

        public DbSet<Wish> Wishes { get; set; }
        public DbSet<WishMedia> WishMedia { get; set; }

        public DbSet<Celebration> Celebrations { get; set; }
        public DbSet<CelebrationMedia> CelebrationMedias { get; set; }

        public DbSet<Announcement> Announcements { get; set; }
    }
}