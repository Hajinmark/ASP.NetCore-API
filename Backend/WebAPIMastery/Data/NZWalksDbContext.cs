using Microsoft.EntityFrameworkCore;
using WebAPIMastery.Controllers;
using WebAPIMastery.Models.Domain;

namespace WebAPIMastery.Data
{
    public class NZWalksDbContext: DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
            
        }

        //DbSet is a property of dbClass that represents the table of a database
        //DbSet->Model->Name of table
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOne(x => x.Detail)
                .WithOne(p => p.Person)
                .HasForeignKey<Detail>(p => p.PersonId);

            base.OnModelCreating(modelBuilder);

            //Seed Data for Difficulties
            var Difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("f0a3cbf7-0f95-4388-94a3-a306cdb6010e"),
                    Name = "Easy"
                },

                new Difficulty()
                {
                    Id = Guid.Parse("3ad069cb-cdb8-4832-9a61-26f0bbde75c4"),
                    Name = "Medium"
                },

                new Difficulty()
                {
                    Id = Guid.Parse("f34e3aed-a8b6-439e-8497-bfae359b8fdb"),
                    Name = "Hard"
                }

            };

            modelBuilder.Entity<Difficulty>().HasData(Difficulties);
            
        }

    }
}
