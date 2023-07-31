using asp_MVC_letsTry.Models.Game_classes;
using asp_MVC_letsTry.Models.Game_help_clasees;
using asp_MVC_letsTry.Models.Helper_classes;
using asp_MVC_letsTry.SessionExtensions;
using asp_MVC_letsTry.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

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
            _session.SetString("Step", "sender");
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
        public async Task FullField([FromBody] string EnemyEmail) //тут null
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
            _session.Set<Game_duel>("game", game);

            return Json(true);
        }
        [HttpPost]
        public async Task<JsonResult> Attacking([FromBody] HelperToAttack position)
        {
            if (position is null)
            {
                return Json(null);
            }

            string? senderStr = _httpContext.Request.Query["sender"];
            bool sender = System.Convert.ToBoolean(senderStr);
            string whichStep = _session.GetString("Step");
            if (sender)
            {
                if (whichStep == "sender")
                {
                    //_session.SetString("Step", "Nonsender");
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                if (whichStep == "Nonsender")
                {
                    //_session.SetString("Step", "sender");
                }
                else
                {
                    return Json(false);
                }
            }

            XY cell = new XY((byte)position.x, (byte)position.y);
            game = _session.Get<Game_duel>("game");
            int isHit = game.second_battle_field.isItHit(cell); // 0 - не влучив, 1 - влучив, -1 - знищив
            _session.Set<Game_duel>("game", game);

            if (isHit == 0)
            {
                if (whichStep == "sender")
                {
                    _session.SetString("Step", "Nonsender");
                }
                else if (whichStep == "Nonsender")
                {
                    _session.SetString("Step", "sender");
                }
                else
                {
                    throw new InvalidDataException();
                }
            }

            var EnemyEmail = _httpContext.Request.Query["enemyEmail"];
            await _hubContext.Clients.User(EnemyEmail).SendAsync("Attack", cell, isHit);

            return Json(isHit);
        }
        [HttpGet]
        public JsonResult MyFieldQuick()
        {
            game = _session.Get<Game_duel>("game");
            game.first_battle_field.createMyField();
            _session.Set<Game_duel>("game", game);
            return Json(game.first_battle_field.ToString());
        }
        [HttpPut]
        public JsonResult SynchrStep()
        {
            lock (this)
            {
                var step = _session.GetString("Step");
                if (step == "Nonsender")
                {
                    _session.SetString("Step", "sender");
                }
                else if (step == "sender")
                {
                    _session.SetString("Step", "Nonsender");
                }
                else
                {
                    throw new InvalidDataException();
                }
                return Json(true);
            }
        }
        [HttpPut]
        public JsonResult SynchrHit([FromBody] XY cell)
        {
            lock (this)
            {
                game = _session.Get<Game_duel>("game");
                int isHit = game.first_battle_field.isItHit(cell); // 0 - не влучив, 1 - влучив, -1 - знищив
                if (isHit == 0)
                {
                    return Json(false);
                }
                _session.Set<Game_duel>("game", game);
                return Json(true);
            }
        }
        public JsonResult deadEnemyShip(int id)
        {
            game = _session.Get<Game_duel>("game");
            ship deadShip = null;
            foreach (ship someShip in game.second_battle_field.ships)
            {
                bool needBreak = false;
                foreach (XY pos in someShip.position)
                {
                    if (pos == new XY((byte)(id / 10), (byte)(id - id / 10 * 10)))
                    {
                        deadShip = someShip;
                        needBreak = true;
                        break;
                    }
                }
                if (needBreak == true)
                {
                    break;
                }
            }

            if (deadShip == null)
            {
                return Json(-1);
            }

            int[][] arr = new int[deadShip.length][];
            for (int i = 0; i < deadShip.length; i++)
            {
                arr[i] = new int[2];
                arr[i][0] = (int)deadShip.position[i]._x;
                arr[i][1] = (int)deadShip.position[i]._y;
            }

            _session.Set<Game_duel>("game", game);

            return Json(arr);
        }
        public JsonResult isEnd(int id) // 0 - мій, 1 - ворога
        {
            lock (this)
            {
                game = _session.Get<Game_duel>("game");
                /*bool isEnd;
                if (id == 0)
                {
                    isEnd = game.second_battle_field.isEnd();
                    // всі кораблі ворога знищені
                }
                else
                {
                    isEnd = game.first_battle_field.isEnd();
                    // всі мої кораблі знищені
                }*/
                bool isEnd = game.first_battle_field.isEnd() || game.second_battle_field.isEnd();
                _session.Set<Game_duel>("game", game);
                return Json(isEnd);
            }
        }
        public JsonResult enemyField()
        {
            game = _session.Get<Game_duel>("game");
            int[][] arr = new int[game.second_battle_field.ships.Count][];
            for (int i = 0; i < game.second_battle_field.ships.Count; i++)
            {
                arr[i] = new int[game.second_battle_field.ships[i].length];

                for (int j = 0; j < game.second_battle_field.ships[i].length; j++)
                {
                    arr[i][j] = (int)game.second_battle_field.ships[i].position[j]._x * 10 + (int)game.second_battle_field.ships[i].position[j]._y;
                }
            }
            _session.Set<Game_duel>("game", game);

            return Json(arr);
        }
    }
}