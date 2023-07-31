using asp_MVC_letsTry.DataBase;
using asp_MVC_letsTry.MyAutoMapper;
using asp_MVC_letsTry.SignalR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.SignalR;

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

            builder.Services.AddSignalR(hubOptions =>
            {
                hubOptions.EnableDetailedErrors = true;
                hubOptions.ClientTimeoutInterval = TimeSpan.FromMinutes(10);
                hubOptions.KeepAliveInterval = TimeSpan.FromMinutes(10);
                hubOptions.HandshakeTimeout = TimeSpan.FromMinutes(10);
            });

            builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt => opt.LoginPath = "/logInForm/LogIn");
            builder.Services.AddAuthentication();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            //? - вроді як wwwroot щоб точно юзати, але точно хз, тіпа якщо в інших файлах є тоже веб-файли
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapHub<Chat>("/chat", options =>
            {
                options.LongPolling.PollTimeout = TimeSpan.FromMinutes(5.5);
                options.TransportSendTimeout = TimeSpan.FromMinutes(5.5);
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=singUpForm}/{action=Create}/{id?}");

            app.Run();
        }
    }
}