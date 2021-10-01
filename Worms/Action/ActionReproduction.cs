using System.Collections.Generic;
using Worms.GameModel;

namespace Worms.Action
{
    public class ActionReproduction : Action
    {
        private readonly Direction _direction;

        public ActionReproduction(Direction direction)
        {
            _direction = direction;
        }

        public void Execute(Worm worm, List<Worm> worms, List<Food> foods)
        {
            int nextX = worm.X, nextY = worm.Y;

            // Этот кусок кода повторяется 43534 раз, как лучше?
            switch (_direction)
            {
                case Direction.Down:
                    nextX++;
                    break;
                case Direction.Left:
                    nextY--;
                    break;
                case Direction.Right:
                    nextY++;
                    break;
                case Direction.Up:
                    nextX--;
                    break;
            }

            foreach (var curWorm in worms)
            {
                if (curWorm.X == nextX && curWorm.Y == nextY)
                    throw new ReproductionWormException(
                        $"Worm {worm.Name} can't reproduction {_direction.ToString()}"
                    );
            }
            
            foreach (var food in foods)
            {
                if (food.X == nextX && food.Y == nextY)
                    throw new ReproductionWormException(
                        $"Worm {worm.Name} can't reproduction {_direction.ToString()}"
                    );
            }

            worm.Reproduction();

            worms.Add(new Worm($"{worm.Name}'s son", nextX, nextY, Const.StartHealthNewWorm));

            worm.Reproduction();
        }
    }
}