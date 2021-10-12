using System.Collections.Generic;
using Worms.GameModel;

namespace Worms.Action
{
    public class ActionReproduction : IAction
    {
        private readonly Direction _direction;

        public ActionReproduction(Direction direction)
        {
            _direction = direction;
        }

        public void Execute(Worm worm, List<Worm> worms)
        {
            int nextX = worm.X, nextY = worm.Y;
            
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

            worm.Reproduction();

            worms.Add(new Worm($"{worm.Name}'s son", nextX, nextY, Const.StartHealthNewWorm));

            worm.Reproduction();
        }
    }
}