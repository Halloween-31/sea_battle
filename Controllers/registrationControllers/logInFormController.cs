using asp_MVC_letsTry.DataBase;
using asp_MVC_letsTry.Models.registrationForms;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_MVC_letsTry.Controllers.registrationControllers
{
    public class logInFormController : Controller
    {
        private readonly AppDB_Content _context;
        private readonly IMapper _mapper;

        public logInFormController(AppDB_Content context, IMapper mapping)
        {
            _context = context;
            _mapper = mapping;
        }

        // GET: singUpForm/Create
        public IActionResult LogIn()
        {
            return View("../registrationViews/logInForm/LogIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name,surname,password,email")] logInForm user)
        {
            if (ModelState.IsValid)
            {
                var Users = await _context.Users.ToListAsync();
                foreach (var every in Users)
                {
                    if (every.email == user.email && user.password == every.password)
                    {
                        return RedirectToAction("HomePage", "user", new { id = every.id });
                    }
                    ModelState.AddModelError(string.Empty, "Неправильна пошта чи пароль!");
                }             
            }
            return View("../registrationViews/logInForm/LogIn", user);
        }
    }
}
