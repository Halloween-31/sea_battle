using asp_MVC_letsTry.Models;
using asp_MVC_letsTry.Models.Game_classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using asp_MVC_letsTry.Models.loginForms_try;
using asp_MVC_letsTry.Models.registrationForms;
using AutoMapper;

namespace asp_MVC_letsTry.Controllers.loginForms_try
{
    public class loginForm_tryController : Controller
    {
        //public loginForm _loginForm;
        private readonly IMapper _mapper;
        public loginForm_tryController(IMapper mapper) 
        {
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(loginForm_try __loginForm)
        {
            if (ModelState.IsValid)
            {
                //this._loginForm = __loginForm;
                return RedirectToAction("GameField", "Game");       //("loginGame");
            }

            return View(__loginForm);
        }

        //[HttpGet]
        public ActionResult loginGame(loginForm_try __loginForm)
        {
            /*var smth = new ship(4);
            smth.position = new List<XY> {
                new XY(1,1),
                new XY(1,2),
                new XY(1,3),
                new XY(1,4),
            };

            Response.WriteAsJsonAsync(smth);*/


            /*var response = context.Response;
            var request = context.Request;
            if (request.Path == "/loginForm/loginGame")
            {
                var message = "Некорректные данные";   // содержание сообщения по умолчанию
                try
                {
                    // пытаемся получить данные json
                    var form = await request.ReadFromJsonAsync<loginForm>();
                    if (form != null) // если данные сконвертированы в Person
                        message = $"Name: {form.name}  Surname: {form.surname}";
                }
                catch { }
                // отправляем пользователю данные
                await response.WriteAsJsonAsync(new { text = message });
            }
            else
            {
                response.ContentType = "text/html; charset=utf-8";
                await response.SendFileAsync("html/index.html");
            }*/



            return View(__loginForm);
        }

        /*[HttpPost]
        public ActionResult loginGame(loginForm __loginForm, HttpContext context)
        {

            answerToCon(context);

            return View(__loginForm);
        }*/

        [HttpPost]
        public JsonResult resFunc([FromBody] loginForm_try data) //[FromBody] обов'язково, бо без нього не працюєч
        {
            //loginForm someForm = new loginForm();

            //var obj = JsonConverter<loginForm>.Read(Utf8JsonReader, string, data);

            return Json(data);
        }

        /*private async Task answerToCon(HttpContext context)
        {
            var response = context.Response;
            var request = context.Request;
            if (request.Path == "/loginForm/loginGame")
            {
                var message = "Некорректные данные";   // содержание сообщения по умолчанию
                try
                {
                    // пытаемся получить данные json
                    var form = await request.ReadFromJsonAsync<loginForm>();
                    if (form != null)   // если данные сконвертированы в Person
                    {
                        message = $"Name: {form.name}  Surname: {form.surname}";
                    }
                }
                catch { }
                // отправляем пользователю данные
                await response.WriteAsJsonAsync(new { text = message });
            }
            else
            {
                response.ContentType = "text/html; charset=utf-8";
                await response.SendFileAsync("Views/loginForm/loginGame");
            }
        }*/






        /*
        // GET: loginFormController
        public ActionResult Index()
        {
            return View();
        }

        // GET: loginFormController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: loginFormController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: loginFormController/Create
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

        // GET: loginFormController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: loginFormController/Edit/5
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

        // GET: loginFormController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: loginFormController/Delete/5
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
        */
    }
}
