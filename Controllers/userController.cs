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

namespace asp_MVC_letsTry.Controllers
{    
    public class userController : Controller
    {
        private readonly AppDB_Content _context;
        private readonly ISession _session;
        private int? id;
        private int? CurrentUserId
        {
            get 
            {
                if(!id.HasValue)
                {
                    return -1;
                }    
                else
                {
                    return id;
                }
            } 
            set
            {
                if(!id.HasValue) 
                {
                    id = value;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }
        
        public userController(AppDB_Content context, IHttpContextAccessor session)
        {
            _context = context;            
            _session = session.HttpContext.Session;
        }                

        public async Task<IActionResult> HomePage(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }
            CurrentUserId = id;
            _session.SetInt32("id", (int)CurrentUserId);
            
            return View(user);                          
        }

        [HttpGet]
        public JsonResult GetCurrentUserId()
        {
            return Json(_session.GetInt32("id"));            
        }
    }
}
