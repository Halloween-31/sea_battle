using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Reflection.Metadata.Ecma335;

namespace asp_MVC_letsTry.Models.Game_clasees
{
    public class battle_field
    {
        public List<ship> ships { get; set; }

        private static bool isConnected = false;
        private static byte choise;
        public bool full = false;

        private static bool isHit = false;
        private static int[,] arr = new int[10, 10];
        private static bool[] direct = new bool[4] { true, true, true, true };
        private List<XY> hited = new List<XY>();
        private ship hitedShip = new ship();
        private static int indexOfFollowing = 0;
        public battle_field()
        {
            ships = new List<ship>
            {
                new ship(4),
                new ship(3),
                new ship(3),
                new ship(2),
                new ship(2),
                new ship(2),
                new ship(1),
                new ship(1),
                new ship(1),
                new ship(1)
            };

            isConnected = false;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    arr[i, j] = -1;
                }
            }
        }
        private byte rand(byte choise) 
        {
            // 1 - 0-9; 0 - 0-1
            var randomer = new Random();
            byte ret = 15;
            if (choise == 1)
            {
                ret = (byte)randomer.Next(0, 9);
            }
            if(choise == 0)
            {
                ret = (byte)(randomer.Next(0, 9) % 2);
            }
            //randomer.NextBytes(ret);
            return ret;
        }
        public bool checkUniquePos(XY cell) // конери
        {
            foreach (ship someShip in ships)
            {
                foreach (XY xy in someShip.position)
                {
                    XY temp = new XY(cell);

                    temp._x--;
                    temp._y--;
                    if (xy == temp)
                    {
                        return false;
                    }
                    temp._x++;
                    temp._y++;

                    temp._x++;
                    temp._y++;
                    if (xy == temp)
                    {
                        return false;
                    }
                    temp._x--;
                    temp._y--;

                    temp._x--;
                    temp._y++;
                    if (xy == temp)
                    {
                        return false;
                    }
                    temp._x++;
                    temp._y--;

                    temp._x++;
                    temp._y--;
                    if (xy == temp)
                    {
                        return false;
                    }
                    temp._x++;
                    temp._y--;
                }
            }

            return true;
        }
        public bool checkCorners(XY cell) //сусіди, я просто помилився
        {
            foreach (ship someShip in ships)
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
                        return false;
                    }
                    temp._x--;

                    temp._x--;
                    if (xy == temp)
                    {
                        return false;
                    }
                    temp._x++;

                    temp._y--;
                    if (xy == temp)
                    {
                        return false;
                    }
                    temp._y++;

                    temp._y++;
                    if (xy == temp)
                    {
                        return false;
                    }
                    temp._y--;
                }
            }

            return true;
        }
        public bool lastCheck(XY cell)
        {
            bool fullField = true;
            foreach (ship someShip in ships)
            {
                if (someShip.full == true)
                {
                    continue;
                }
                fullField = false;

                bool secondBreak = false;
                byte indexOfEl = 0;

                for (int i = 0; i < someShip.length; i++)
                {
                    indexOfEl = (byte)i;

                    if (isConnected == false)
                    {
                        if (someShip.position[i] == new XY(11, 11))
                        {
                            someShip.position[i] = new XY(cell);
                            secondBreak = true;

                            if (someShip.length != 1)
                            {
                                isConnected = true;
                            }

                            break;
                        }
                    }

                    if (isConnected == true)
                    {
                        if (someShip.position[i] == new XY(11, 11))
                        {
                            XY temp = new XY(cell);

                            foreach (XY xy in someShip.position)
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

                            if (secondBreak == false)
                            {
                                return false;
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
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool allIn(XY cell)
        {
            /*if (
                    ((checkUniquePos(cell) == true) &&
                    (checkCorners(cell) == true)) &&
                    (lastCheck(cell) == true))
            {
                return true;
            }
            else
            {
                return false;
            } */
            foreach (ship someShip in this.ships)
            {
                foreach (XY xy in someShip.position)
                {
                    if (xy == cell)
                    {
                        return false;
                    }
                }
            }

            if (this.checkUniquePos(cell) == false)
            {
                return false;
            }
            if (this.checkCorners(cell) == false)
            {
                return false;
            }
            if (this.lastCheck(cell) == false)
            {
                return false;
            }
            return true;
        }
        public void creatingField()
        {
            for (int i = 0; i < this.ships.Count; i++)
            {
                //треба пройтися по позиціях


                choise = rand(0);

                for (int j = 0; j < this.ships[i].length; j++)
                {
                    if (j == 0)
                    {
                        while (true)
                        {
                            byte tempX = this.rand(1);
                            byte tempY = this.rand(1);

                            XY cell = new XY(tempX, tempY);

                            if (allIn(cell) == true)
                            {
                                /*this.ships[i].position[j]._x = tempX;
                                this.ships[i].position[j]._y = tempY;*/

                                break;
                            }

                        }
                    }




                    byte temp_X;
                    byte temp_Y;

                    if (j != 0)
                    {
                        while (true)
                        {
                            if (choise == 0)
                            {
                                // по х
                                temp_X = (byte)this.ships[i].position[j - 1]._x;    //this.ships[i].position[j]._x = this.ships[i].position[j - 1]._x;
                                if (rand(0) == 0)
                                {
                                    if ((this.ships[i].position[j - 1]._y + 1) != 10)
                                    {
                                        temp_Y = (byte)(this.ships[i].position[j - 1]._y + 1);    //this.ships[i].position[j]._y = (byte)(this.ships[i].position[j - 1]._y + 1);
                                    }
                                    else
                                    {
                                        byte? lessY = this.ships[i].position[j - 1]._y;
                                        foreach (XY pos in this.ships[i].position)
                                        {
                                            if ((pos._y != null) && (pos._y < lessY))
                                            {
                                                lessY = pos._y;
                                            }
                                        }
                                        temp_Y = (byte)(lessY - 1);   //this.ships[i].position[j]._y = (byte)(lessY - 1);
                                    }
                                }
                                else
                                {
                                    if ((this.ships[i].position[j - 1]._y - 1) >= 0)
                                    {
                                        temp_Y = (byte)(this.ships[i].position[j - 1]._y - 1);    //this.ships[i].position[j]._y = (byte)(this.ships[i].position[j - 1]._y - 1);
                                    }
                                    else
                                    {
                                        byte? mostY = this.ships[i].position[j - 1]._y;
                                        foreach (XY pos in this.ships[i].position)
                                        {
                                            if ((pos._y != null) && (pos._y > mostY))
                                            {
                                                mostY = pos._y;
                                            }
                                        }
                                        temp_Y = (byte)(mostY - 1);   //this.ships[i].position[j]._y = (byte)(mostY - 1);
                                    }
                                }
                            }
                            else
                            {
                                // по у
                                temp_Y = (byte)this.ships[i].position[j - 1]._y;    //this.ships[i].position[j]._y = this.ships[i].position[j - 1]._y;
                                if (rand(0) == 0)
                                {
                                    if ((this.ships[i].position[j - 1]._x + 1) != 10)
                                    {
                                        temp_X = (byte)(this.ships[i].position[j - 1]._x + 1);    //this.ships[i].position[j]._x = (byte)(this.ships[i].position[j - 1]._x + 1);
                                    }
                                    else
                                    {
                                        byte? lessX = this.ships[i].position[j - 1]._x;
                                        foreach (XY pos in this.ships[i].position)
                                        {
                                            if ((pos._x != null) && (pos._x < lessX))
                                            {
                                                lessX = pos._x;
                                            }
                                        }
                                        temp_X = (byte)(lessX - 1);   //this.ships[i].position[j]._x = (byte)(lessX - 1);
                                    }
                                }
                                else
                                {
                                    if ((this.ships[i].position[j - 1]._x - 1) >= 0)
                                    {
                                        temp_X = (byte)(this.ships[i].position[j - 1]._x - 1);    //this.ships[i].position[j]._x = (byte)(this.ships[i].position[j - 1]._x - 1);
                                    }
                                    else
                                    {
                                        byte? mostX = this.ships[i].position[j - 1]._x;
                                        foreach (XY pos in this.ships[i].position)
                                        {
                                            if ((pos._x != null) && (pos._x > mostX))
                                            {
                                                mostX = pos._x;
                                            }
                                        }
                                        temp_X = (byte)(mostX + 1);   //this.ships[i].position[j]._x = (byte)(mostX + 1);
                                    }
                                }
                            }


                            XY cell = new XY(temp_X, temp_Y);
                            if (allIn(cell) == true)
                            {
                                break;
                            }
                            else 
                            {
                                if(rand(0) == 0)
                                {
                                    if (this.ships[i].position[0]._x == this.ships[i].position[1]._x)
                                    {
                                        // по х
                                        if(temp_Y > this.ships[i].position[0]._y)
                                        {
                                            if((this.ships[i].position[0]._y - 1) >= 0)
                                            {
                                                temp_Y = (byte)(this.ships[i].position[0]._y - 1);
                                            }
                                            else
                                            {
                                                //спочатку будуємо корабель
                                                j = 0;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if ((this.ships[i].position[0]._y + 1) != 10)
                                            {
                                                temp_Y = (byte)(this.ships[i].position[0]._y + 1);
                                            }
                                            else
                                            {
                                                j = 0;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //по у
                                        if (temp_X > this.ships[i].position[0]._x)
                                        {
                                            if ((this.ships[i].position[0]._x - 1) >= 0)
                                            {
                                                temp_X = (byte)(this.ships[i].position[0]._x - 1);
                                            }
                                            else
                                            {
                                                j = 0;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if ((this.ships[i].position[0]._x + 1) != 10)
                                            {
                                                temp_X = (byte)(this.ships[i].position[0]._x + 1);
                                            }
                                            else
                                            {
                                                j = 0;
                                                break;
                                            }
                                        }
                                    }

                                    cell = new XY(temp_X, temp_Y);
                                    if (allIn(cell) == true)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    if(choise == 0)
                                    {
                                        choise = 1;
                                    }
                                    else
                                    {
                                        choise = 0;
                                    }                                   
                                }
                            }
                        }
                    }
                }

                this.ships[i].full = true;
            }



            // записуємо у файл розташування кораблів
            StreamWriter shipsLocation = new StreamWriter("ships.txt", false);
            shipsLocation.WriteLine(this.ToString());
            shipsLocation.Close();
            //














                //foreach (ship _ship in this.ships)
                //{
                /*do
                {
                    _ship.position[0]._x = rand(1);
                    _ship.position[0]._y = rand(1); //там сука було [1]

                    isConnected = true;
                } while (allIn(_ship.position[0]) == false);  */

                /*while(true)
                {
                    _ship.position[0]._x = rand(1);
                    _ship.position[0]._y = rand(1);

                    if (allIn(_ship.position[0]) == true)
                    {
                        break;
                    }
                }*/
                //}



                /*foreach(ship someShip in ships)
                {
                    for(int i = (someShip.length - 1); i >= 0; i--)
                    {
                        /*do
                        {
                            if (i == (someShip.length - 1))
                            {
                                someShip.position[i]._x = rand(1);
                                someShip.position[i]._y = rand(1);

                                isConnected = true;
                            }

                        } while ( (this.checkUniquePos(someShip.position[i]) == false) ||
                        (this.checkCorners(someShip.position[i]) == false)   ||
                        (this.lastCheck(someShip.position[i]) == false));*/






                /*if(i != (someShip.length - 1))
                {
                    if(choise == 0)
                    {
                        // по х
                        someShip.position[i]._x = someShip.position[i + 1]._x;
                        if(rand(0) == 0)
                        {
                            if ((someShip.position[i + 1]._y + 1) != 10)
                            {
                                someShip.position[i]._y = (byte)(someShip.position[i + 1]._y + 1);
                            }
                            else
                            {
                                byte? lessY = someShip.position[i + 1]._y;
                                foreach (XY pos in someShip.position)
                                {
                                    if ((pos._y != null) && (pos._y < lessY))
                                    {
                                        lessY = pos._y;
                                    }
                                }
                                someShip.position[i]._y = (byte)(lessY - 1);
                            }
                        }
                        else
                        {
                            if ((someShip.position[i + 1]._y - 1) != -1)
                            {
                                someShip.position[i]._y = (byte)(someShip.position[i + 1]._y - 1);
                            }
                            else
                            {
                                byte? mostY = someShip.position[i + 1]._y;
                                foreach (XY pos in someShip.position)
                                {
                                    if ((pos._y != null) && (pos._y > mostY))
                                    {
                                        mostY = pos._y;
                                    }
                                }
                                someShip.position[i]._y = (byte)(mostY - 1);
                            }
                        }
                    }
                    else
                    {
                        // по у
                        someShip.position[i]._y = someShip.position[i + 1]._y;
                        if (rand(0) == 0)
                        {
                            if ((someShip.position[i + 1]._x + 1) != 10)
                            {
                                someShip.position[i]._x = (byte)(someShip.position[i + 1]._x + 1);  
                            }
                            else
                            {
                                byte? lessX = someShip.position[i + 1]._x;
                                foreach(XY pos in someShip.position)
                                {
                                    if((pos._x != null) && (pos._x < lessX))
                                    {
                                        lessX = pos._x;
                                    }
                                }
                                someShip.position[i]._x = (byte)(lessX - 1);
                            }
                        }
                        else
                        {
                            if ((someShip.position[i + 1]._x - 1) != -1)
                            {
                                someShip.position[i]._x = (byte)(someShip.position[i + 1]._x - 1);
                            }
                            else
                            {
                                byte? mostX = someShip.position[i + 1]._x;
                                foreach (XY pos in someShip.position)
                                {
                                    if ((pos._x != null) && (pos._x > mostX))
                                    {
                                        mostX = pos._x;
                                    }
                                }
                                someShip.position[i]._x = (byte)(mostX + 1);
                            }
                        }
                    }*/
                //}


        }
        public void createMyField()
        {
            this.ships[0].position = new List<XY> {
                new XY(0, 0),
                new XY(0, 1),
                new XY(0, 2),
                new XY(0, 3)
            };
            this.ships[1].position = new List<XY> {
                new XY(2, 0),
                new XY(2, 1),
                new XY(2, 2)
            };
            this.ships[2].position = new List<XY> {
                new XY(4, 0),
                new XY(4, 1),
                new XY(4, 2)
            };
            this.ships[3].position = new List<XY> {
                new XY(6, 0),
                new XY(6, 1)
            };
            this.ships[4].position = new List<XY> {
                new XY(8, 0),
                new XY(8, 1)
            };
            this.ships[5].position = new List<XY> {
                new XY(9, 9),
                new XY(9, 8)
            };
            this.ships[6].position = new List<XY> {
                new XY(0, 9)
            };
            this.ships[7].position = new List<XY> {
                new XY(2, 9)
            };
            this.ships[8].position = new List<XY> {
                new XY(4, 9)
            };
            this.ships[9].position = new List<XY> {
                new XY(6, 9)
            };
        }
        public void isFull()        // можна заміниити не async, а івентами
        {
            foreach (ship someShip in this.ships)
            {
                foreach (XY pos in someShip.position)
                {
                    if(pos == new XY(11, 11))
                    {
                        this.full = false;
                        return;
                    }
                }
            }
            this.full = true;
        }
        public int isItHit(XY cell) // 0 - не влучив, 1 - влучив, -1 - знищив; для ворога аналогічно   
        {
            foreach(ship someShip in this.ships)
            {
                if(someShip.isAlive == false)
                {
                    continue;
                }
                foreach (XY pos in someShip.position)
                {
                    if(pos == cell)
                    {
                        pos.hited = true;
                        someShip.isAliveShip();
                        if (someShip.isAlive == false)
                        {
                            return -1;
                        }
                        return 1;
                    }
                }
            }
            return 0;
        }
        public List<XY> startEnemyAttacking()           // обмеження на -- та ++
        {
            bool canGo = true;
            //int indexOfList = 0;

            List<XY> final = new List<XY>();

            while(canGo)
            {
                canGo = false;

                if (isHit == false)
                {
                    XY newPos;
                    while (true)
                    {
                        newPos = new XY(this.rand(1), this.rand(1));     // зробити так шоб не повтоялося

                        bool needBreak = true;
                        for (int i = 0; i < 10; i++)
                        {
                            for (int j = 0; j < 10; j++)
                            {
                                if (newPos == new XY((byte)(arr[i, j] / 10), (byte)(arr[i, j] - ((arr[i, j] / 10) * 10) )))
                                {
                                    needBreak = false;
                                    break;
                                }
                            }
                            if (needBreak == false)
                            {
                                break;
                            }
                        }
                        arr[(int)newPos._x, (int)newPos._y] = ((int)newPos._x) * 10 + ((int)newPos._y);
                        if (needBreak == true)
                        {
                            break;
                        }

                    }



                    foreach (ship someShip in this.ships)
                    {
                        bool needBreak = false;
                        foreach (XY pos in someShip.position)
                        {
                            if (pos == newPos)
                            {
                                pos.hited = true;
                                isHit = true;

                                hited.Add(pos);
                                hitedShip = someShip;
                                canGo = true;
                                needBreak = true;

                                hitedShip.isAliveShip();
                                if (hitedShip.isAlive == false)
                                {
                                    makeBlackZone(ref arr);

                                    isHit = false;

                                    indexOfFollowing = 0;
                                    List<XY> res = new List<XY>(hited);
                                    hited.Clear();
                                    hitedShip = null;
                                    direct = new bool[4] { true, true, true,  true };
                                    res.Add(new XY(11, 11));
                                    return res;
                                }

                                break;
                            }
                        }
                        if (needBreak == true)
                        {
                            break;
                        }
                    }                 

                    if (canGo == false)
                    {
                        return new List<XY> { newPos };
                    }
                }
                else
                {
                    if (hited.Count == 1)
                    { 
                        canGo = false;

                        byte one = this.rand(0);
                        if (one == 0)
                        {
                            //  по х
                            byte two = this.rand(0);
                            if ((two == 0) && (direct[0] != false))
                            {
                                // позаду, 0
                                XY nextPos = new XY(hited[hited.Count - 1]);
                                if((nextPos._x - 1) != -1)
                                {
                                    nextPos._x--;
                                }
                                else
                                {
                                    canGo = true;
                                    continue;
                                }

                                /*bool needBreak = false;
                                for (int i = 0; i < 10; i++)
                                {
                                    for (int j = 0; j < 10; j++)
                                    {
                                        if (nextPos == new XY((byte)(arr[i, j] / 10), (byte)(arr[i, j] - arr[i, j] / 10)))
                                        {
                                            direct[0] = false;
                                            needBreak = true;
                                            break;
                                        }
                                    }
                                    if (needBreak == true)
                                    {
                                        break;
                                    }
                                }
                                arr[(int)nextPos._x, (int)nextPos._y] = ((int)nextPos._x) * 10 + ((int)nextPos._y);
                                if (needBreak == true)
                                {
                                    canGo = true;
                                    break;
                                }

                                foreach (XY pos in hitedShip.position)
                                {
                                    if (pos == nextPos)
                                    {
                                        hited.Add(pos);
                                        pos.hited = true;
                                        canGo = true;
                                        break;
                                    }
                                }*/

                                if (BaseChecking(nextPos, ref canGo) == false)
                                {
                                    break;
                                }

                                if (canGo == false)
                                {
                                    direct[0] = false;
                                    //hited.Add(nextPos);
                                    List<XY> res = new List<XY>(hited);
                                    res.Add(nextPos);
                                    return res;
                                }
                            }
                            else if ((two == 1) && (direct[2] != false))
                            {
                                // попереду, 2

                                XY nextPos = new XY(hited[hited.Count - 1]);
                                if((nextPos._x + 1) != 10)
                                {
                                    nextPos._x++;
                                }
                                else
                                {
                                    canGo = true;
                                    continue;
                                }

                                /*bool needBreak = false;
                                for (int i = 0; i < 10; i++)
                                {
                                    for (int j = 0; j < 10; j++)
                                    {
                                        if (nextPos == new XY((byte)(arr[i, j] / 10), (byte)(arr[i, j] - arr[i, j] / 10)))
                                        {
                                            direct[2] = false;
                                            needBreak = true;
                                            break;
                                        }
                                    }
                                    if (needBreak == true)
                                    {
                                        break;
                                    }
                                }
                                arr[(int)nextPos._x, (int)nextPos._y] = ((int)nextPos._x) * 10 + ((int)nextPos._y);
                                if (needBreak == true)
                                {
                                    canGo = true;
                                    break;
                                }

                                foreach (XY pos in hitedShip.position)
                                {
                                    if (pos == nextPos)
                                    {
                                        hited.Add(pos);
                                        pos.hited = true;
                                        canGo = true;
                                        break;
                                    }
                                }*/

                                if (BaseChecking(nextPos, ref canGo) == false)
                                {
                                    break;
                                }

                                if (canGo == false)
                                {
                                    direct[2] = false;
                                    //hited.Add(nextPos);
                                    List<XY> res = new List<XY>(hited);
                                    res.Add(nextPos);
                                    return res;
                                }
                            }
                            else
                            {
                                canGo = true;
                            }
                        }
                        else
                        {
                            //  по у 
                            byte two = this.rand(0);
                            if ((two == 0) && (direct[1] != false))
                            {
                                // вверх, 1
                                XY nextPos = new XY(hited[hited.Count - 1]);
                                if ((nextPos._y - 1) != -1)
                                {
                                    nextPos._y--;
                                }
                                else
                                {
                                    canGo = true;
                                    continue;
                                }

                                /*bool needBreak = false;
                                for (int i = 0; i < 10; i++)
                                {
                                    for (int j = 0; j < 10; j++)
                                    {
                                        if (nextPos == new XY((byte)(arr[i, j] / 10), (byte)(arr[i, j] - arr[i, j] / 10)))
                                        {
                                            direct[1] = false;
                                            needBreak = true;
                                            break;
                                        }
                                    }
                                    if (needBreak == true)
                                    {
                                        break;
                                    }
                                }
                                arr[(int)nextPos._x, (int)nextPos._y] = ((int)nextPos._x) * 10 + ((int)nextPos._y);
                                if (needBreak == true)
                                {
                                    canGo = true;
                                    break;
                                }

                                foreach (XY pos in hitedShip.position)
                                {
                                    if (pos == nextPos)
                                    {
                                        hited.Add(pos);
                                        pos.hited = true;
                                        canGo = true;
                                        break;
                                    }
                                }*/

                                if (BaseChecking(nextPos, ref canGo) == false)
                                {
                                    break;
                                }

                                if (canGo == false)
                                {
                                    direct[1] = false;
                                    //hited.Add(nextPos);
                                    List<XY> res = new List<XY>(hited);
                                    res.Add(nextPos);
                                    return res;
                                }
                            }
                            else if ((two == 1) && (direct[3] != false))
                            {
                                // вниз, 3
                                XY nextPos = new XY(hited[hited.Count - 1]);
                                if ((nextPos._y + 1) != 10)
                                {
                                    nextPos._y++;
                                }
                                else
                                {
                                    canGo = true;
                                    continue;
                                }

                                /*bool needBreak = false;
                                for (int i = 0; i < 10; i++)
                                {
                                    for (int j = 0; j < 10; j++)
                                    {
                                        if (nextPos == new XY((byte)(arr[i, j] / 10), (byte)(arr[i, j] - arr[i, j] / 10)))
                                        {
                                            direct[3] = false;
                                            needBreak = true;
                                            break;
                                        }
                                    }
                                    if (needBreak == true)
                                    {
                                        break;
                                    }
                                }
                                arr[(int)nextPos._x, (int)nextPos._y] = ((int)nextPos._x) * 10 + ((int)nextPos._y);
                                if (needBreak == true)
                                {
                                    canGo = true;
                                    break;
                                }

                                foreach (XY pos in hitedShip.position)
                                {
                                    if (pos == nextPos)
                                    {
                                        hited.Add(pos);
                                        pos.hited = true;
                                        canGo = true;
                                        break;
                                    }
                                }*/

                                if (BaseChecking(nextPos, ref canGo) == false)
                                {
                                    break;
                                }

                                if (canGo == false)
                                {
                                    direct[3] = false;
                                    //hited.Add(nextPos);
                                    List<XY> res = new List<XY>(hited);
                                    res.Add(nextPos);
                                    return res;
                                }
                            }
                            else
                            {
                                canGo = true;
                            }
                        }
                    }
                    else
                    {
                        hitedShip.isAliveShip();
                        if (hitedShip.isAlive == true)
                        {
                            if (indexOfFollowing == 1)
                            {
                                indexOfFollowing = 0;

                                XY nextPos = new XY(11, 11);
                                if (hited[0]._x == hited[1]._x)
                                {
                                    if (hited[0]._y < hited[1]._y)
                                    {
                                        if ((hited[0]._y - 1) != -1)
                                        {
                                            nextPos = new XY((byte)hited[0]._x, (byte)(hited[0]._y - 1));
                                        }

                                        if (BaseChecking(nextPos, ref canGo) == false)
                                        {
                                            break;
                                        }

                                        indexOfFollowing++;
                                        final = new List<XY>(hited);
                                        final.Add(nextPos);

                                        continue;
                                    }
                                    else
                                    {
                                        if ((hited[0]._y + 1) != 10)
                                        {
                                            nextPos = new XY((byte)hited[0]._x, (byte)(hited[0]._y + 1));
                                        }

                                        if (BaseChecking(nextPos, ref canGo) == false)
                                        {
                                            break;
                                        }

                                        indexOfFollowing++;
                                        final = new List<XY>(hited);
                                        final.Add(nextPos);

                                        continue;
                                    }
                                }
                                else
                                {
                                    if (hited[0]._x < hited[1]._x)
                                    {
                                        if ((hited[0]._x - 1) != -1)
                                        {
                                            nextPos = new XY((byte)(hited[0]._x - 1), (byte)hited[0]._y);
                                        }

                                        if (BaseChecking(nextPos, ref canGo) == false)
                                        {
                                            break;
                                        }

                                        indexOfFollowing++;
                                        final = new List<XY>(hited);
                                        final.Add(nextPos);

                                        continue;
                                    }
                                    else
                                    {
                                        if ((hited[0]._x + 1) != 10)
                                        {
                                            nextPos = new XY((byte)(hited[0]._x + 1), (byte)hited[0]._y);
                                        }

                                        if (BaseChecking(nextPos, ref canGo) == false)
                                        {
                                            break;
                                        }

                                        indexOfFollowing++;
                                        final = new List<XY>(hited);
                                        final.Add(nextPos);

                                        continue;
                                    }
                                }
                            }





                            // якщо перше проходження

                            canGo = false;

                            if (hited[0]._x == hited[1]._x)
                            {
                                XY nextPos = new XY(hited[hited.Count - 1]);
                                if (hited[0]._y < hited[1]._y)
                                {
                                    if ((nextPos._y + 1) != 10)
                                    {
                                        nextPos._y++;
                                    }
                                    else
                                    {
                                        nextPos = new XY(hited[0]);
                                        nextPos._y--;
                                    }

                                    /*bool needBreak = false;
                                    for (int i = 0; i < 10; i++)
                                    {
                                        for (int j = 0; j < 10; j++)
                                        {
                                            if (nextPos == new XY((byte)(arr[i, j] / 10), (byte)(arr[i, j] - arr[i, j] / 10)))
                                            {
                                                needBreak = true;
                                                break;
                                            }
                                        }
                                        if (needBreak == true)
                                        {
                                            break;
                                        }
                                    }
                                    arr[(int)nextPos._x, (int)nextPos._y] = ((int)nextPos._x) * 10 + ((int)nextPos._y);
                                    if (needBreak == true)
                                    {
                                        canGo = true;
                                        break;
                                    }

                                    foreach (XY pos in hitedShip.position)
                                    {
                                        if (pos == nextPos)
                                        {
                                            hited.Add(pos);
                                            pos.hited = true;
                                            canGo = true;
                                            break;
                                        }
                                    }*/

                                    if (BaseChecking(nextPos, ref canGo) == false)
                                    {
                                        break;
                                    }

                                    final = new List<XY>(hited);
                                    final.Add(nextPos);
                                }
                                else
                                {
                                    nextPos = new XY(hited[hited.Count - 1]);
                                    if ((nextPos._y - 1) != -1)
                                    {
                                        nextPos._y--;
                                    }
                                    else
                                    {
                                        nextPos = new XY(hited[0]);
                                        nextPos._y++;
                                    }

                                    /*bool needBreak = false;
                                    for (int i = 0; i < 10; i++)
                                    {
                                        for (int j = 0; j < 10; j++)
                                        {
                                            if (nextPos == new XY((byte)(arr[i, j] / 10), (byte)(arr[i, j] - arr[i, j] / 10)))
                                            {
                                                needBreak = true;
                                                break;
                                            }
                                        }
                                        if (needBreak == true)
                                        {
                                            break;
                                        }
                                    }
                                    arr[(int)nextPos._x, (int)nextPos._y] = ((int)nextPos._x) * 10 + ((int)nextPos._y);
                                    if (needBreak == true)
                                    {
                                        canGo = true;
                                        break;
                                    }

                                    foreach (XY pos in hitedShip.position)
                                    {
                                        if (pos == nextPos)
                                        {
                                            hited.Add(pos);
                                            pos.hited = true;
                                            canGo = true;
                                            break;
                                        }
                                    }*/

                                    if (BaseChecking(nextPos, ref canGo) == false)
                                    {
                                        break;
                                    }

                                    final = new List<XY>(hited);
                                    final.Add(nextPos);
                                }

                                hitedShip.isAliveShip();
                            }
                            else
                            {
                                XY nextPos = new XY(hited[hited.Count - 1]);
                                if (hited[0]._x < hited[1]._x)
                                {
                                    if ((nextPos._x + 1) != 10)
                                    {
                                        nextPos._x++;
                                    }
                                    else
                                    {
                                        nextPos = new XY(hited[0]);
                                        nextPos._x--;
                                    }

                                    /*bool needBreak = false;
                                    for (int i = 0; i < 10; i++)
                                    {
                                        for (int j = 0; j < 10; j++)
                                        {
                                            if (nextPos == new XY((byte)(arr[i, j] / 10), (byte)(arr[i, j] - arr[i, j] / 10)))
                                            {
                                                needBreak = true;
                                                break;
                                            }
                                        }
                                        if (needBreak == true)
                                        {
                                            break;
                                        }
                                    }
                                    arr[(int)nextPos._x, (int)nextPos._y] = ((int)nextPos._x) * 10 + ((int)nextPos._y);
                                    if (needBreak == true)
                                    {
                                        canGo = true;
                                        break;
                                    }

                                    foreach (XY pos in hitedShip.position)
                                    {
                                        if (pos == nextPos)
                                        {
                                            hited.Add(pos);
                                            pos.hited = true;
                                            canGo = true;
                                            break;
                                        }
                                    }*/

                                    if (BaseChecking(nextPos, ref canGo) == false)
                                    {
                                        break;
                                    }

                                    final = new List<XY>(hited);
                                    final.Add(nextPos);
                                }
                                else
                                {
                                    nextPos = new XY(hited[hited.Count - 1]);
                                    if ((nextPos._x - 1) != -1)
                                    {
                                        nextPos._x--;
                                    }
                                    else
                                    {
                                        nextPos = new XY(hited[0]);
                                        nextPos._x++;
                                    }

                                    /*bool needBreak = false;
                                    for (int i = 0; i < 10; i++)
                                    {
                                        for (int j = 0; j < 10; j++)
                                        {
                                            if (nextPos == new XY((byte)(arr[i, j] / 10), (byte)(arr[i, j] - arr[i, j] / 10)))
                                            {
                                                needBreak = true;
                                                break;
                                            }
                                        }
                                        if (needBreak == true)
                                        {
                                            break;
                                        }
                                    }
                                    arr[(int)nextPos._x, (int)nextPos._y] = ((int)nextPos._x) * 10 + ((int)nextPos._y);
                                    if (needBreak == true)
                                    {
                                        canGo = true;
                                        break;
                                    }

                                    foreach (XY pos in hitedShip.position)
                                    {
                                        if (pos == nextPos)
                                        {
                                            hited.Add(pos);
                                            pos.hited = true;
                                            canGo = true;
                                            break;
                                        }
                                    }*/

                                    if (BaseChecking(nextPos, ref canGo) == false)
                                    {
                                        break;
                                    }

                                    final = new List<XY>(hited);
                                    final.Add(nextPos);
                                }

                                hitedShip.isAliveShip();
                            }
                        }
                        else
                        {
                            makeBlackZone(ref arr);

                            isHit = false;
                            //hited.Add(new XY(11, 11));
                            indexOfFollowing = 0;
                            List<XY> res = new List<XY>(hited);
                            hited.Clear();
                            hitedShip = null;
                            direct = new bool[4] { true, true, true,  true };
                            res.Add(new XY(11, 11));
                            return res;
                        }
                    }                   
                }
            }

            indexOfFollowing++;
            return final;
        }
        private bool BaseChecking(XY nextPos, ref bool canGo)
        {
            bool needBreak = false;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (nextPos == new XY((byte)(arr[i, j] / 10), (byte)(arr[i, j] - ((arr[i, j] / 10) * 10)   )))
                    {
                        needBreak = true;
                        break;
                    }
                }
                if (needBreak == true)
                {
                    break;
                }
            }
            arr[(int)nextPos._x, (int)nextPos._y] = ((int)nextPos._x) * 10 + ((int)nextPos._y);
            if (needBreak == true)
            {
                canGo = true;
                return false;
            }

            foreach (XY pos in hitedShip.position)
            {
                if (pos == nextPos)
                {
                    hited.Add(pos);
                    pos.hited = true;
                    canGo = true;
                    break;
                }
            }

            return true;
        }
        private string ToString()
        {
            string res = "";
            foreach (ship sh in this.ships)
            {
                res += sh.ToString();
                res += "\n";
            }
            return res;
        }
        private void makeBlackZone(ref int[,] arr)
        {
            for (int k = 0; k < hitedShip.length; k++)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if((i == -1) && (((int)hitedShip.position[k]._x + i) == -1))
                        {
                            continue;
                        }
                        if((i == 1) && (((int)hitedShip.position[k]._x + i) == 10))
                        {
                            continue;
                        }
                        if((j == -1) && (((int)hitedShip.position[k]._y + j) == -1))
                        {
                            continue;
                        }
                        if ((j == 1) && (((int)hitedShip.position[k]._y + j) == 10))
                        {
                            continue;
                        }

                        if (((i != 0) || (j != 0)) && (arr[(int)hitedShip.position[k]._x + i, (int)hitedShip.position[k]._y + j] == -1))
                        {
                            arr[(int)hitedShip.position[k]._x + i, (int)hitedShip.position[k]._y + j] = ((int)hitedShip.position[k]._x + i) * 10 + ((int)hitedShip.position[k]._y + j);
                        }
                    }
                }
            }   
        }
        public bool isEnd() // чи є ще живі кораблі
        {
            foreach (ship someShip in this.ships)
            {
                foreach (XY pos in someShip.position)
                {
                    if(pos.hited == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}