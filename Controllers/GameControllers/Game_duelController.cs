using asp_MVC_letsTry.Models.Game_classes;
using asp_MVC_letsTry.Models.Game_help_clasees;
using asp_MVC_letsTry.Models.Helper_classes;
using asp_MVC_letsTry.SessionExtensions;
using asp_MVC_letsTry.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.SignalR;
using Microsoft.JSInterop.Implementation;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace asp_MVC_letsTry.Controllers.GameControllers
{
    public class Game_duelController : Controller
    {
        private Game_duel? game;
        private readonly HttpContext _httpContext;
        private readonly ISession _session;        
        private readonly IHubContext<Chat> _hubContext;
        public Game_duelController(IHttpContextAccessor accessor, IHubContext<Chat> hubContext)
        {
            _httpContext = accessor.HttpContext;
            _session = accessor.HttpContext.Session;
            _hubContext = hubContext;
        }
        public ActionResult GameField()
        {
            game = new Game_duel();
            _session.Set<Game_duel>("game", game);
            return View("../GameViews/Game_duel/GameField");
        }
        [HttpPost]
        public JsonResult clickMyBtn([FromBody] HelperToCreateMyField xy_pos)
        {
            game = _session.Get<Game_duel>("game");
            if (xy_pos is null)
            {
                return Json(0);
            }

            string senderStr = _httpContext.Request.Query["sender"];
            bool sender = System.Convert.ToBoolean(senderStr);
            battle_field field;
            if (sender == true)
            {
                field = game.first_battle_field;
            }
            else if (sender == false)
            {
                field = game.second_battle_field;
            }
            else
            {
                throw new Exception();
            }

            field.isFull();
            if (field.full)
            {
                return Json(-1);
            }

            XY cell = new XY(xy_pos.x is not null ? (byte)xy_pos.x : throw new Exception(), xy_pos.y is not null ? (byte)xy_pos.y : throw new Exception());

            int isThisNewCell = game.checkEverything(cell, field); // 0 - notNew, 1 - new
            _session.Set<Game_duel>("game", game);

            return Json(isThisNewCell);
        }
        [HttpGet]
        public JsonResult checkEnemyFull()
        {
            string senderStr = _httpContext.Request.Query["sender"];
            bool sender = System.Convert.ToBoolean(senderStr);
            battle_field field;
            //беремо поле іншого гравця
            if (sender == true)
            {
                field = game.second_battle_field;
            }
            else if (sender == false)
            {                
                field = game.first_battle_field;
            }
            else
            {
                throw new Exception();
            }

            field.isFull();
            if (field.full)
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        [HttpPut]
        public async Task FullField([FromBody]string EnemyEmail) //тут null
        {
            game = _session.Get<Game_duel>("game");
            await _hubContext.Clients.User(EnemyEmail).SendAsync("FullField", JsonConvert.SerializeObject(game.first_battle_field));
        }
        [HttpPut]
        public JsonResult EnemyField([FromBody] string EnemyField)
        {            
            var _EnemyField = JsonConvert.DeserializeObject<battle_field>(EnemyField, 
                new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace });

            game = _session.Get<Game_duel>("game");
            game.second_battle_field = _EnemyField;

            return Json(true);
        }
    }
}
