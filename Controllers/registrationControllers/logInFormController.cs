using asp_MVC_letsTry.DataBase;
using asp_MVC_letsTry.Models.registrationForms;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace asp_MVC_letsTry.Controllers.registrationControllers
{
    public class logInFormController : Controller
    {
        private readonly AppDB_Content _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _http_context;

        public logInFormController(AppDB_Content context, IMapper mapping, IHttpContextAccessor http_context)
        {
            _context = context;
            _mapper = mapping;
            _http_context = http_context;
        }

        // GET: singUpForm/Create
        public IActionResult LogIn()
        {
            return View("../registrationViews/logInForm/LogIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("password,email")] logInForm user, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var Users = await _context.Users.ToListAsync();
                foreach (var every in Users)
                {
                    if (every.email == user.email && user.password == every.password)
                    {
                        var claims = new List<Claim> { new Claim(ClaimTypes.Email, every.email),
                            new Claim(ClaimTypes.Name, every.name),
                            new Claim(ClaimTypes.Surname, every.surname)
                        };

                        // создаем объект ClaimsIdentity
                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "MyCookiesAuthType");
                        // установка аутентификационных куки
                        await _http_context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                        return returnUrl is not null ? Redirect(returnUrl) : RedirectToAction("HomePage", "user", new { id = every.id });
                    }
                }             
                ModelState.AddModelError(string.Empty, "Неправильна пошта чи пароль!");
            }
            return View("../registrationViews/logInForm/LogIn", user);
        }
    }
}
