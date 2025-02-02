using App.Domain.Core.Entites;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using App.Infra.Db.Sql.Configrations;
using Task = App.Domain.Core.Entites.Task;

namespace App.Infra.Db.Sql
{
    public class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TaskConfigration());

            base.OnModelCreating(modelBuilder);
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer
        //        (@"Server=LAPTOP-GM2D722B; Initial Catalog=Hw-22; User Id=sa; Password=13771377; TrustServerCertificate=True;");
        //}

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }

}

