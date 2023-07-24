using asp_MVC_letsTry.Models.Game_help_clasees;

namespace asp_MVC_letsTry.Models.Game_classes
{
    public class Game_duel
    {
        public battle_field first_battle_field;
        public battle_field second_battle_field;
        public Game_duel() 
        {
            first_battle_field = new battle_field();
            second_battle_field = new battle_field();
        }
        public int checkEverything(XY cell, battle_field field)
        {
            foreach (ship someShip in field.ships)
            {
                foreach (XY xy in someShip.position)
                {
                    if (xy == cell)
                    {
                        return 0;
                    }
                }
            }

            bool unique = field.checkUniquePos(cell);
            if (!unique)
            {
                return 0;
            }

            bool corner = field.checkCorners(cell);
            if (!corner)
            {
                return 0;
            }

            bool last = field.lastCheck(cell);
            if (last == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        public override string ToString()
        {
            string res = "";
            res += first_battle_field.ToString();
            res += second_battle_field.ToString();
            return res;
        }
    }
}