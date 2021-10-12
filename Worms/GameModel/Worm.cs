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

        public Action.IAction ChooseAction(World world)
        {
            Food chosenFood = null;
            foreach (var food in world.Foods)
            {
                if (chosenFood == null || Math.Abs(food.X - _x) + Math.Abs(food.Y - _y) <
                    Math.Abs(chosenFood.X - _x) + Math.Abs(chosenFood.Y - _y))
                {
                    chosenFood = food;
                }
            }

            IAction action = new ActionNothing();

            if (chosenFood != null)
            {
                if (chosenFood.X - _x < 0)
                {
                    action = new ActionMove(Direction.Up);
                }
                if (chosenFood.X - _x > 0)
                {
                    action = new ActionMove(Direction.Down);
                }
                if (chosenFood.Y - _y < 0)
                {
                    action = new ActionMove(Direction.Left);
                }
                if (chosenFood.Y - _y > 0)
                {
                    action = new ActionMove(Direction.Right);
                }
            }

            return action;
        }

        private Action.IAction WalkInCircles()
        {
            Action.IAction action = new ActionNothing();

            if (_x == 10)
            {
                action = new ActionMove(Direction.Right);

                if (_y == 10)
                {
                    action = new ActionMove(Direction.Up);
                }
            }

            if (_y == 10)
            {
                action = new ActionMove(Direction.Up);

                if (_x == 0)
                {
                    action = new ActionMove(Direction.Left);
                }
            }

            if (_x == 0)
            {
                action = new ActionMove(Direction.Left);

                if (_y == 0)
                {
                    action = new ActionMove(Direction.Down);
                }
            }

            if (_y == 0)
            {
                action = new ActionMove(Direction.Down);

                if (_x == 10)
                {
                    action = new ActionMove(Direction.Right);
                }
            }

            return action;
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