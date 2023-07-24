using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asp_MVC_letsTry.DataBase;
using asp_MVC_letsTry.Models;
using asp_MVC_letsTry.Models.registrationForms;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace asp_MVC_letsTry.Controllers.registrationControllers
{
    public class singUpFormController : Controller
    {
        private readonly AppDB_Content _context;
        private readonly IMapper _mapper;

        public singUpFormController(AppDB_Content context, IMapper mapping)
        {
            _context = context;
            _mapper = mapping;
        }       

        // GET: singUpForm/Create
        public IActionResult Create()
        {
            return View("../registrationViews/singUpForm/Create");
        }

        // POST: singUpForm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name,surname,password,email")] signUpForm user, IHttpContextAccessor context, string? returnUrl)
        {
            if (ModelState.IsValid)
            {
                var Users = await _context.Users.ToListAsync();
                foreach (var every in Users)
                {
                    if(every.email == user.email)
                    {
                        ModelState.AddModelError(string.Empty, "Дана пошта вже зареєстрована!");
                        return View("../registrationViews/singUpForm/Create", user);
                    }
                }
                var newUser = _mapper.Map<user>(user);

                _context.Add(newUser);
                await _context.SaveChangesAsync();

                var claims = new List<Claim> { new Claim(ClaimTypes.Email, newUser.email),
                    new Claim(ClaimTypes.Name, newUser.name),
                    new Claim(ClaimTypes.Surname, newUser.surname)
                };

                // создаем объект ClaimsIdentity
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "MyCookiesAuthType");
                // установка аутентификационных куки
                await context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));


                return returnUrl is not null ? Redirect(returnUrl) : RedirectToAction("HomePage", "user", new { id = newUser.id });
            }
            return View("../registrationViews/singUpForm/Create", user);
        }
    }
}
