using Newtonsoft.Json;

namespace asp_MVC_letsTry.Models.Game_help_clasees
{
    public class ship
    {
        public byte length { get; set; }
        public List<XY> position { get; set; }
        [JsonProperty] public bool full = false;
        [JsonProperty] public bool isAlive = true;
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
        public static bool operator==(ship a, ship b)
        {
            if(a.position.Count != b.position.Count)
            {
                return false;
            }
            if(a.length != b.length)
            {
                return false;
            }
            for (int i = 0; i < a.length; i++)
            {
                if (a.position[i] != b.position[i])
                {
                    return false;
                }
            }
            return true;
        }
        public static bool operator !=(ship a, ship b)
        {
            if(a == b)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
