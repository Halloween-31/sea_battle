using asp_MVC_letsTry.Models.Game_help_clasees;

namespace asp_MVC_letsTry.Models.Game_classes
{
    public class Game
    {
        public battle_field my_battle_field;
        public battle_field enemy_battle_field;
        //public static bool isConnected;

        public Game()
        {
            my_battle_field = new battle_field();
            enemy_battle_field = new battle_field();


            enemy_battle_field.creatingField();

            //isConnected = false;
        }

        public int checkEverything(XY cell)
        {
            foreach (ship someShip in my_battle_field.ships)
            {
                foreach (XY xy in someShip.position)
                {
                    if (xy == cell)
                    {
                        return 0;
                    }
                }
            }

            // позиція унікальна

            /*foreach (ship someShip in my_battle_field.ships)
            {
                foreach (XY xy in someShip.position)
                {
                    XY temp = new XY(cell);

                    temp._x--;
                    temp._y--;
                    if(xy == temp)
                    {
                        return 0;
                    }
                    temp._x++;
                    temp._y++;

                    temp._x++;
                    temp._y++;
                    if (xy == temp)
                    {
                        return 0;
                    }
                    temp._x--;
                    temp._y--;

                    temp._x--;
                    temp._y++;
                    if (xy == temp)
                    {
                        return 0;
                    }
                    temp._x++;
                    temp._y--;

                    temp._x++;
                    temp._y--;
                    if (xy == temp)
                    {
                        return 0;
                    }
                    temp._x++;
                    temp._y--;
                }
            }*/

            bool unique = my_battle_field.checkUniquePos(cell);
            if (!unique)
            {
                return 0;
            }

            //позиця немає сусідів по кутах

            /*foreach (ship someShip in my_battle_field.ships)
            {
                if ((someShip.full == false) && (someShip.length != 1))
                {
                    continue;
                }

                foreach (XY xy in someShip.position)
                {
                    XY temp = new XY(cell);

                    temp._x++;
                    if (xy == temp)
                    {
                       return 0;
                    }
                    temp._x--;

                    temp._x--;
                    if (xy == temp)
                    {
                        return 0;
                    }
                    temp._x++;

                    temp._y--;
                    if (xy == temp)
                    {
                        return 0;
                    }
                    temp._y++;

                    temp._y++;
                    if (xy == temp)
                    {
                        return 0;
                    }
                    temp._y--;
                }
            }*/

            bool corner = my_battle_field.checkCorners(cell);
            if (!corner)
            {
                return 0;
            }

            //позиція немає сусідів по  боках

            /*bool fullField = true;
            foreach (ship someShip in my_battle_field.ships)
            {
                if(someShip.full == true)
                {
                    continue;
                }
                fullField = false;

                bool secondBreak = false;
                byte indexOfEl = 0;

                for(int i = 0; i < someShip.length; i++)
                {
                    indexOfEl = (byte)i;
                        
                    if (isConnected == false)
                    {
                        if (someShip.position[i] == new XY(11, 11))
                        {
                            someShip.position[i] = new XY(cell);
                            secondBreak = true;

                            if(someShip.length != 1)
                            {
                                isConnected = true;
                            }
                            
                            break;
                        }
                    }

                    if(isConnected == true)
                    {
                        if (someShip.position[i] == new XY(11, 11))
                        {
                            XY temp = new XY(cell);

                            foreach(XY xy in someShip.position)
                            {
                                if (someShip.position[1]._x == 11)
                                {
                                    temp._x++;
                                    if (xy == temp)
                                    {
                                        someShip.position[i] = new XY(cell);
                                        secondBreak = true;

                                        break;
                                    }
                                    temp._x--;

                                    temp._x--;
                                    if (xy == temp)
                                    {
                                        someShip.position[i] = new XY(cell);
                                        secondBreak = true;

                                        break;
                                    }
                                    temp._x++;

                                    temp._y--;
                                    if (xy == temp)
                                    {
                                        someShip.position[i] = new XY(cell);
                                        secondBreak = true;

                                        break;
                                    }
                                    temp._y++;

                                    temp._y++;
                                    if (xy == temp)
                                    {
                                        someShip.position[i] = new XY(cell);
                                        secondBreak = true;

                                        break;
                                    }
                                    temp._y--;
                                }
                                else 
                                {
                                    if (someShip.position[0]._x == someShip.position[1]._x)
                                    {
                                        temp._y--;
                                        if (xy == temp)
                                        {
                                            someShip.position[i] = new XY(cell);
                                            secondBreak = true;

                                            break;
                                        }
                                        temp._y++;

                                        temp._y++;
                                        if (xy == temp)
                                        {
                                            someShip.position[i] = new XY(cell);
                                            secondBreak = true;

                                            break;
                                        }
                                        temp._y--;
                                    }
                                    else if (someShip.position[0]._y == someShip.position[1]._y)
                                    {
                                        temp._x--;
                                        if (xy == temp)
                                        {
                                            someShip.position[i] = new XY(cell);
                                            secondBreak = true;

                                            break;
                                        }
                                        temp._x++;

                                        temp._x++;
                                        if (xy == temp)
                                        {
                                            someShip.position[i] = new XY(cell);
                                            secondBreak = true;

                                            break;
                                        }
                                        temp._x--;
                                    }
                                }
                            }

                            if(secondBreak == false)
                            {
                                return 0;
                            }
                        }
                    }

                    if (secondBreak == true)
                    {
                        break;
                    }
                }

                if ((indexOfEl + 1) == someShip.length)
                {
                    someShip.full = true;
                    isConnected = false;
                }

                if (secondBreak == true)
                {
                    break;
                }
            }

            // позицію додано
            if (fullField == true)
            {
                return 0;
            }
            else
            {
                return 1;
            }*/

            bool last = my_battle_field.lastCheck(cell);
            if (last == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
