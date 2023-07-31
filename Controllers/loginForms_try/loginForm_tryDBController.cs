using asp_MVC_letsTry.DataBase;
using asp_MVC_letsTry.Models.loginForms_try;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace asp_MVC_letsTry.Controllers.loginForms_try
{
    public class loginForm_tryDBController : Controller
    {
        private readonly AppDB_Content _context;

        public loginForm_tryDBController(AppDB_Content context)
        {
            _context = context;
        }







        // GET: loginForm
        public async Task<IActionResult> Index()
        {
            return _context.loginForm_tryDB != null ?
                        View(await _context.loginForm_tryDB.ToListAsync()) :
                        Problem("Entity set 'AppDB_Content.loginForm'  is null.");
        }

        // GET: loginForm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.loginForm_tryDB == null)
            {
                return NotFound();
            }

            var loginForm = await _context.loginForm_tryDB
                .FirstOrDefaultAsync(m => m.id == id);
            if (loginForm == null)
            {
                return NotFound();
            }

            return View(loginForm);
        }

        // GET: loginForm/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: loginForm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,surname,password,email")] loginForm_tryDB loginForm_tryDB) // [Bind("id,name,surname,password,email")]
        {
            if (ModelState.IsValid)
            {
                _context.Add(loginForm_tryDB);
                await _context.SaveChangesAsync();
                //
                //loginForm.id = loginForm.id;
                //
                return RedirectToAction(nameof(Index));
            }
            return View(loginForm_tryDB);
        }

        // GET: loginForm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.loginForm_tryDB == null)
            {
                return NotFound();
            }

            var loginForm = await _context.loginForm_tryDB.FindAsync(id);
            if (loginForm == null)
            {
                return NotFound();
            }
            return View(loginForm);
        }

        // POST: loginForm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,surname,password,email")] loginForm_tryDB loginForm) // [Bind("id,name,surname,password,email")]
        {
            if (id != loginForm.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loginForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!loginFormExists(loginForm.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(loginForm);
        }

        // GET: loginForm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.loginForm_tryDB == null)
            {
                return NotFound();
            }

            var loginForm = await _context.loginForm_tryDB
                .FirstOrDefaultAsync(m => m.id == id);
            if (loginForm == null)
            {
                return NotFound();
            }

            return View(loginForm);
        }

        // POST: loginForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.loginForm_tryDB == null)
            {
                return Problem("Entity set 'AppDB_Content.loginForm'  is null.");
            }
            var loginForm = await _context.loginForm_tryDB.FindAsync(id);
            if (loginForm != null)
            {
                _context.loginForm_tryDB.Remove(loginForm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool loginFormExists(int id)
        {
            return (_context.loginForm_tryDB?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
