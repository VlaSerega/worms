using System;

namespace Worms
{
    public class Simulator
    {
        private readonly World _world;

        public Simulator(int heightField, int widthField)
        {
            _world = new World();
            _world.AddWorm(new Worm("Федор", 0, 0));
        }

        public void Run()
        {
            while (_world.MoveNumber != Const.MaxMoveNumber)
            {
                MakeWormsStep();

                foreach (var food in _world.Foods)
                {
                    food.ReduceHealth();
                }

                _world.Worms.RemoveAll(worm => worm.Health == 0);
                _world.Foods.RemoveAll(food => food.Health == 0);
                _world.IncreaseMoveNumber();
            }
        }

        private bool ValidAction(Action action, Worm worm)
        {
            bool valid = true;
            int nextX = worm.X, nextY = worm.Y;
            
            switch (action)
            {
                case Action.Down:
                    nextX++;
                    break;
                case Action.Up:
                    nextY--;
                    break;
                case Action.Left:
                    nextY--;
                    break;
                case Action.Right:
                    nextY++;
                    break;
            }

            foreach (var selectedWorm in _world.Worms)
            {
                if (selectedWorm.X == nextX && selectedWorm.Y == nextY)
                {
                    valid = false;
                    break;
                }
            }

            return valid;
        }

        private void MakeWormsStep()
        {
            foreach (var worm in _world.Worms)
            {
                var action = worm.ChooseAction(_world);

                if (ValidAction(action, worm))
                {
                    worm.DoAction(action);
                    SteppedOnFood(worm);
                }

                worm.ReduceHealth();
                Console.WriteLine(worm);
            }
        }

        private void SteppedOnFood(Worm worm)
        {
            Food eatenFood = null;
            
            foreach (var food in _world.Foods)
            {
                if (food.X == worm.X && food.Y == worm.Y)
                {
                    worm.Eat();
                    eatenFood = food;
                }
            }

            if (eatenFood != null)
            {
                _world.Foods.Remove(eatenFood);
            }
        }
    }
}