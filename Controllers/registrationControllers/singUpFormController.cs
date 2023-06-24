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
        public async Task<IActionResult> Create([Bind("name,surname,password,email")] signUpForm user)
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
                return RedirectToAction("HomePage", "user", new {id = newUser.id});
            }
            return View("../registrationViews/singUpForm/Create", user);
        }
    }
}
