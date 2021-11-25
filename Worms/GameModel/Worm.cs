using System;
using Worms.Action;

namespace Worms.GameModel
{
    public class Worm
    {
        private readonly string _name;

        private int _x;
        private int _y;

        private int _health;

        public Worm(string name, int x, int y, int health = Const.StartHealthWorm)
        {
            _name = name;
            _x = x;
            _y = y;
            _health = health;
        }

        public IAction ChooseAction(WorldState world)
        {
            return world.Behavior.GetAction(this, world);
        }

        public void Eat()
        {
            _health += Const.HealthForFood;
        }

        public void Reproduction()
        {
            if (_health > Const.HealthForReproduction)
                _health -= Const.HealthForReproduction;
            else
            {
                throw new ReproductionWormException(
                    $"Health of worm {_name} is {_health}, that less then {Const.HealthForReproduction}"
                );
            }
        }

        public void Move(ActionMove action)
        {
            switch (action.Direction)
            {
                case Direction.Down:
                    _x++;
                    break;
                case Direction.Left:
                    _y--;
                    break;
                case Direction.Right:
                    _y++;
                    break;
                case Direction.Up:
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