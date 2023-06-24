using System.Data.Common;
using System.Xml.Linq;

namespace asp_MVC_letsTry.Models.Game_help_clasees
{
    public class XY
    {
        public byte? _x { get; set; }
        public byte? _y { get; set; }

        // змінна для попадання
        public bool hited = false;
        //

        /*public XY()
        {
            _x = null;
            _y = null;
        }*/
        public XY(byte x, byte y)
        {
            _x = x;
            _y = y;
        }
        public XY(XY toCopy)
        {
            _x = toCopy._x;
            _y = toCopy._y;
        }
        
        /*public XY(int pos)
        {
            _x = (byte)(pos / 10);
            _y = (byte)(pos - ((pos / 10) * 10));
        }

        public XY(string pos)
        {
            int iPos = Int32.Parse(pos);
            _x = (byte)(iPos / 10);
            _y = (byte)(iPos - ((iPos / 10) * 10));
        }*/

        public override string ToString()
        {
            string? res = Convert.ToString(this._x);
            res += Convert.ToString(this._y);
            return res;
        }



        // може треба буде ф-ю notEqual
        // чекнути чи можна в шарпах перевантажувати оператори


        public static bool operator==(XY first, XY second)
        {
            /*if(first._x == null && first._y == null && second._x == null && second._y == null)
            {
                return true;
            }*/

            if(first._x == second._x)
            {
                if(first._y == second._y)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool operator!=(XY first, XY second)
        {
            if (first._x == second._x)
            {
                if (first._y == second._y)
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object? obj)
        {
            if(obj is not null)
            {
                return obj is not null ? this == (XY)obj : false;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
