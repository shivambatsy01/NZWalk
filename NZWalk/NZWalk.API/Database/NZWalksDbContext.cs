using Microsoft.EntityFrameworkCore;
using NZWalk.API.Models.Domain;

namespace NZWalk.API.Database
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options) : base(options) 
        {
            //ctro of derived class is calling ctor of base class
            //ctor of base class is a parametrised ctor which is taking params as Dbcontextoptions<type derived class>
            //that's why we created a parametrized ctor of derived class
        }

        //create Db properties based on Models we created
        //Entity framework take care of it
        //Entity framework will create these tables on first time running (if tables doesn't exists)
        //however we have made our first migration through nuget package manager console

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //need to create model for user_role manually as it is complex

            modelBuilder.Entity<User_Roles>()
                .HasOne(x => x.Role)  //nav prop in user_roles
                .WithMany(y => y.UserRoles) //nav prop in Role
                .HasForeignKey(x => x.RoleId);//foriegn key to connect with Roles table

            modelBuilder.Entity<User_Roles>()
                .HasOne(x => x.User)  //nav prop in user_roles
                .WithMany(y => y.UserRoles)  //nav prop in User
                .HasForeignKey(x => x.UserId); //foriegn key to connect with users table
        }


        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User_Roles> UserRoles { get; set; }

    }
}
