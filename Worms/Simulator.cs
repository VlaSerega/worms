using System;

namespace Worms
{
    public class Simulator
    {
        private World _world;

        public Simulator(int heightField, int widthField)
        {
            _world = new World(heightField, widthField);
            _world.AddWorm(new Worm("Федор", 0, 0));
        }

        public void Run()
        {
            while (_world.MoveNumber != Const.MaxMoveNumber)
            {
                MakeWormsStep();

                _world.Worms.RemoveAll(worm => worm.Health == 0);
                _world.Foods.RemoveAll(food => food.Health == 0);
                _world.IncreaseMoveNumber();
            }
        }

        private bool ValidAction(Action action, Worm worm)
        {
            switch (action)
            {
                case Action.Down:
                    return worm.X + 1 < _world.Field.Height;
                case Action.Up:
                    return worm.X - 1 >= 0;
                case Action.Left:
                    return worm.Y + 1 >= 0;
                case Action.Right:
                    return worm.Y + 1 < _world.Field.Width;
            }

            return false;
        }

        private void MakeWormsStep()
        {
            foreach (var worm in _world.Worms)
            {
                var action = worm.ChooseAction(_world);

                if (ValidAction(action, worm))
                {
                    worm.DoAction(action);
                }

                worm.ReduceHealth();
                Console.WriteLine(worm);
            }
        }
    }
}