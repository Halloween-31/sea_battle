using Microsoft.AspNetCore.Server.IIS.Core;
using System.Transactions;

namespace asp_MVC_letsTry.Models.Game_help_clasees
{
    public class ship
    {
        public byte length { get; set; }
        public List<XY> position { get; set; }
        public bool full = false;
        public bool isAlive = true;
        public ship()
        {
            length = 0;
            position = new List<XY>();
        }
        public ship(byte _length)
        {
            length = _length;
            position = new List<XY>(length);
            for (int i = 0; i < length; i++)
            {
                position.Add(new XY(11, 11));
            }
        }    
        public void isAliveShip() // можна заміниити не async, а івентами
        {
            for (int i = 0; i < this.length; i++)
            {
                if (this.position[i].hited == false)
                {
                    this.isAlive = true;
                    return;
                }
            }
            this.isAlive = false;
        }
        public override string ToString()
        {
            string res = "";
            foreach (XY pos in position)
            {
                res += pos.ToString();
                res += " ";
            }
            return res;
        }
    }
}
