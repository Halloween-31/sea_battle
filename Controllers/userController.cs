using asp_MVC_letsTry.DataBase;
using asp_MVC_letsTry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp_MVC_letsTry.Controllers
{
    public class userController : Controller
    {
        private readonly AppDB_Content _context;
        private readonly ISession _session;
        private readonly IResponseCookies _cookies;
        private int? id;
        private int? CurrentUserId
        {
            get
            {
                if (!id.HasValue)
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
                if (!id.HasValue)
                {
                    id = value;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public userController(AppDB_Content context, IHttpContextAccessor accessor)
        {
            _context = context;
            _session = accessor.HttpContext.Session;
            _cookies = accessor.HttpContext.Response.Cookies;
        }

        [Authorize]
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
            _session.SetString("isLoggined", "true");

            _cookies.Append("MyEmail", user.email);

            return View(user);
        }

        [HttpGet]
        public JsonResult GetCurrentUserId()
        {
            return Json(_session.GetInt32("id"));
        }
        [HttpPut]
        public async Task SetCurrentConId([FromBody] string? id)
        {
            int? userID = _session.GetInt32("id");
            if (userID.HasValue)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(m => m.id == userID);
                if (user != null)
                {
                    //user.conId = id; //chat.GetConnectionId();
                }
                _context.SaveChanges();
            }
        }
        [HttpPost]
        public async Task<JsonResult> IsEmailExists([FromBody] string? email)
        {
            var find = await _context.Users.FirstOrDefaultAsync(m => m.email == email);
            return find is null ? Json(false) : Json(true);
        }
        [HttpPost]
        public async Task<JsonResult> GetMyName([FromBody] string? email)
        {
            var find = await _context.Users.FirstOrDefaultAsync(m => m.email == email);
            return find is null ? Json(null) : Json(new { find.name, find.surname });
        }
        [HttpPost]
        public async Task<JsonResult> GetMyEmail([FromBody] int? id)
        {
            var find = await _context.Users.FirstOrDefaultAsync(m => m.id == id);
            return find is null ? Json(null) : Json(find.email);
        }
    }
}