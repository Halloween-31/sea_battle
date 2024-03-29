﻿using asp_MVC_letsTry.DataBase;
using asp_MVC_letsTry.Models.Game_classes;
using asp_MVC_letsTry.Models.Game_help_clasees;
using asp_MVC_letsTry.Models.Helper_classes;
using asp_MVC_letsTry.SessionExtensions;
using Microsoft.AspNetCore.Mvc;

namespace asp_MVC_letsTry.Controllers.GameControllers
{
    public class GameController : Controller
    {
        private Game? game;
        private readonly AppDB_Content _context;
        private readonly ISession _session;
        public GameController(AppDB_Content context, IHttpContextAccessor session)
        {
            _context = context;
            _session = session.HttpContext.Session;
        }
        public ActionResult GameField()
        {
            game = new Game();
            game.enemy_battle_field.creatingField();
            _session.Set<Game>("game", game);

            // очищення фалйу
            /*StreamWriter atacking = new StreamWriter("attack.txt", false);
            atacking.WriteLine("");
            atacking.Close();*/

            return View("../GameViews/Game/GameField");
        }

        [HttpGet]
        public JsonResult isLoggined()
        {
            return Json(_session.GetString("isLoggined"));
        }

        [HttpPost]
        public JsonResult click([FromBody] HelperToCreateMyField data)
        {
            game = _session.Get<Game>("game");

            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));


                data = new HelperToCreateMyField();
                data.answer = 0;
                return Json(data);
            }

            game.my_battle_field.isFull();
            if (game.my_battle_field.full == true)
            {
                data.answer = 0;
                return Json(data);
            }            

            XY cell = new XY(data.x is not null ? (byte)data.x : throw new Exception(), data.y is not null ? (byte)data.y : throw new Exception());

            /*if(data.answer == 1)
            {
                data.answer = -1;
                return Json(data);
            }*/

            int? isThisNewCell = game?.checkEverything(cell); // 0 - notNew, 1 - new
            _session.Set<Game>("game", game);

            if (isThisNewCell.HasValue == false)
            {
                throw new InvalidDataException();
            }

            if (isThisNewCell == 1)
            {
                data.answer = 1;
                return Json(data);
            }
            else
            {
                data.answer = 0;
                return Json(data);
            }
        }
        public bool checkIsFull()
        {
            game = _session.Get<Game>("game");
            game.my_battle_field.isFull();
            _session.Set<Game>("game", game);
            return game.my_battle_field.full;
        }
        [HttpPost]
        public ActionResult checkEnemy([FromBody] int[][] data)
        {
            game = _session.Get<Game>("game");

            int[][] mas = new int[10][];

            //data = new List<HelperToCheckEnemy>();

            int i = 0;
            foreach (ship someShip in game.enemy_battle_field.ships)
            {
                mas[i] = new int[someShip.length];


                int j = 0;
                foreach (XY pos in someShip.position)
                {
                    int full = (int)pos._x * 10 + (int)pos._y;
                    mas[i][j] = full;

                    j++;
                }
                i++;
            }

            /*data = new int[10][];
            for (int k = 0; k < 10; k++)
            {
                data[k] = new int[4];
                for (int j = 0; j < 4; j++)
                {
                    data[k][j] = mas[k, j];
                }
            }*/

            _session.Set<Game>("game", game);

            return Json(mas);
        }
        public bool renew()
        {
            game = _session.Get<Game>("game");

            game.my_battle_field = new battle_field();
            game.enemy_battle_field = new battle_field();
            game.enemy_battle_field.creatingField();

            _session.Set<Game>("game", game);

            return true;
        }        
        public void myField()
        {
            game = _session.Get<Game>("game");

            game.my_battle_field.createMyField();

            _session.Set<Game>("game", game);
        }
        public JsonResult attacking([FromBody] HelperToAttack data)
        {
            game = _session.Get<Game>("game");

            XY cell = new XY((byte)data.x, (byte)data.y);

            int isHit = game.enemy_battle_field.isItHit(cell); // 0 - не влучив, 1 - влучив, -1 - знищив

            _session.Set<Game>("game", game);

            return Json(isHit);

            //починається ворожа атака            
        }
        public ActionResult attackAnswer()
        {
            game = _session.Get<Game>("game");

            List<XY> res = game.my_battle_field.startEnemyAttacking();

            if (res.Count == 0)
            {
                return Json(-1);
            }

            if (res[0]._x == 12)
            {
                return Json(-2);
            }

            int[][] arr = new int[res.Count][];
            for (int i = 0; i < res.Count; i++)
            {
                arr[i] = new int[2];
                arr[i][0] = (int)res[i]._x;
                arr[i][1] = (int)res[i]._y;
            }

            // запис атаки ворога
            /*StreamWriter atacking = new StreamWriter("attack.txt", true);
            for (int i = 0; i < res.Count; i++)
            {
                atacking.WriteLine(arr[i][0].ToString() + arr[i][1].ToString());
            }
            atacking.Close();*/
            //

            _session.Set<Game>("game", game);

            return Json(arr);
        }
        public ActionResult isEnd(int id) // 0 - мій, 1 - ворога
        {
            game = _session.Get<Game>("game");

            bool isEnd;
            if (id == 0)
            {
                isEnd = game.my_battle_field.isEnd();
            }
            else
            {
                isEnd = game.enemy_battle_field.isEnd();
            }

            _session.Set<Game>("game", game);

            return Json(isEnd);
        }
        public ActionResult enemyField()
        {
            game = _session.Get<Game>("game");

            int[][] arr = new int[game.enemy_battle_field.ships.Count][];
            for (int i = 0; i < game.enemy_battle_field.ships.Count; i++)
            {
                arr[i] = new int[game.enemy_battle_field.ships[i].length];

                for (int j = 0; j < game.enemy_battle_field.ships[i].length; j++)
                {
                    arr[i][j] = (int)game.enemy_battle_field.ships[i].position[j]._x * 10 + (int)game.enemy_battle_field.ships[i].position[j]._y;
                }
            }

            _session.Set<Game>("game", game);

            return Json(arr);
        }
        public ActionResult deadEnemyShip(int id)
        {
            game = _session.Get<Game>("game");

            ship deadShip = null;
            foreach (ship someShip in game.enemy_battle_field.ships)
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

            if (deadShip is null)
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

            _session.Set<Game>("game", game);

            return Json(arr);
        }
    }
}
