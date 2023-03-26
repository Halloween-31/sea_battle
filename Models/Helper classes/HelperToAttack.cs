namespace asp_MVC_letsTry.Models.Helper_classes
{
    public class HelperToAttack
    {
        public int? x { get; set; }
        public int? y { get; set; }
        public int[]? answer { get; set; }
        public HelperToAttack()
        {
            x = null;
            y = null;
            answer = null;
        }
        public HelperToAttack(int? x, int? y, int[]? answer)
        {
            this.x = x;
            this.y = y;
            this.answer = answer;
        }
    }
}
