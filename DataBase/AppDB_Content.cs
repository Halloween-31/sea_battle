﻿using asp_MVC_letsTry.Models;
using asp_MVC_letsTry.Models.loginForms_try;
using Microsoft.EntityFrameworkCore;

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
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=seabattle_users;Trusted_Connection=True;");
        }
    }
}
