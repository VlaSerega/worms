namespace Worms
{
    public class Worm
    {
        private readonly string _name;

        private int _x;
        private int _y;

        private int _health = Const.StartHealthWorm;

        public Worm(string name, int x, int y)
        {
            _name = name;
            _x = x;
            _y = y;
        }

        public Action ChooseAction(World world)
        {
            Action action = Action.Nothing;

            if (_x == world.Field.Height - 1)
            {
                action = Action.Right;

                if (_y == world.Field.Width - 1)
                {
                    action = Action.Up;
                }
            }

            if (_y == world.Field.Width - 1)
            {
                action = Action.Up;

                if (_x == 0)
                {
                    action = Action.Left;
                }
            }

            if (_x == 0)
            {
                action = Action.Left;

                if (_y == 0)
                {
                    action = Action.Down;
                }
            }

            if (_y == 0)
            {
                action = Action.Down;

                if (_x == world.Field.Height - 1)
                {
                    action = Action.Right;
                }
            }

            return action;
        }

        public void DoAction(Action action)
        {
            switch (action)
            {
                case Action.Right:
                    _y++;
                    break;
                case Action.Left:
                    _y--;
                    break;
                case Action.Down:
                    _x++;
                    break;
                case Action.Up:
                    _x--;
                    break;
            }
        }

        public override string ToString()
        {
            return "Worm[Name = \"" + _name + "\", X = " + _x + ", Y = " + _y + ", Health = " + _health + "]";
        }

        public void ReduceHealth()
        {
            if (_health != 0)
                _health--;
        }

        public int Health => _health;

        public string Name => _name;

        public int X => _x;

        public int Y => _y;
    }
}