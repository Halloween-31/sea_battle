using asp_MVC_letsTry.Models.Game_clasees;

namespace asp_MVC_letsTry.Models.Helper_classes
{
    public class HelperToCreateMyField
    {
        //public XY xy { get; set; }
        public int? x { get; set; }
        public int? y { get; set; }
        public int? answer { get; set; }
        //public int[,]? arr { get; set; }

        public HelperToCreateMyField()
        {
            x = null;
            y = null;
            answer = null;
            //arr = new int[10, 4];
        }
        public HelperToCreateMyField(int? x, int? y, int? answer)//, int[,]? arr)
        {
            this.x = x;
            this.y = y;
            this.answer = answer;

            /*this.arr = new int[10, 4];
            this.arr = arr;*/
        }
    }
}
