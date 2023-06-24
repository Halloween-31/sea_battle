using asp_MVC_letsTry.Models;
using Microsoft.EntityFrameworkCore;
using asp_MVC_letsTry.Models.loginForms_try;
using asp_MVC_letsTry.Models.registrationForms;

namespace asp_MVC_letsTry.DataBase
{
    public class AppDB_Content : DbContext
    {
        public DbSet<user>? Users { get; set; }
        public DbSet<loginForm_tryDB>? loginForm_tryDB { get; set; }

        public AppDB_Content()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=myDataBase;Trusted_Connection=True;");
        }        
    }
}
