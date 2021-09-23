using System.Collections.Generic;
using Worms.GameModel;

namespace Worms.Action
{
    public class ActionMove : Action
    {
        private readonly Direction _direction;

        public ActionMove(Direction direction)
        {
            _direction = direction;
        }

        public void Execute(Worm worm, List<Worm> worms)
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
                    throw new MoveWormException($"Worm {worm.Name} can't move {_direction.ToString()}");
            }

            worm.Move(this);
        }

        public Direction Direction => _direction;
    }
}