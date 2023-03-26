using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace asp_MVC_letsTry.Controllers
{
    public class example : Controller
    {
        // GET: example
        public ActionResult Index()
        {
            return View();
        }

        // GET: example/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: example/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: example/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: example/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: example/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: example/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: example/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
