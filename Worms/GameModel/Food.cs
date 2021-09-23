namespace Worms.GameModel
{
    public class Food
    {
        private readonly int _x;
        private readonly int _y;

        private int _health = Const.StartHealthFood;

        public Food(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public void ReduceHealth()
        {
            if (_health != 0)
                _health--;
        }

        public override string ToString()
        {
            return "Food[X = " + _x + ", Y = " + _y + ", Health = " + _health + "]";
        }

        public int X => _x;

        public int Y => _y;

        public int Health => _health;
    }
}