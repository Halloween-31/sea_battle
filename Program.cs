using asp_MVC_letsTry.DataBase;
using asp_MVC_letsTry.Models;
using Microsoft.AspNetCore.HttpOverrides;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;
using asp_MVC_letsTry.MyAutoMapper;

namespace asp_MVC_letsTry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            AppDB_Content db = new AppDB_Content();
            builder.Services.AddDbContext<AppDB_Content>();

            builder.Services.AddAutoMapper(typeof(MyMapper));

            builder.Services.AddHttpContextAccessor();
            //builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();          

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "MySessionCookie";
                options.IdleTimeout = TimeSpan.FromSeconds(3600);
                options.Cookie.IsEssential = true;
            });

            /*//
            builder.Services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            //*/

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            /*
            app.UseForwardedHeaders();
            */

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=singUpForm}/{action=Create}/{id?}");

            app.Run();
        }
    }
}