using System.Collections.Generic;
using Worms.GameModel;

namespace Worms.Action
{
    public class ActionMove : IAction
    {
        private readonly Direction _direction;

        public ActionMove(Direction direction)
        {
            _direction = direction;
        }

        public void Execute(Worm worm, WorldState world)
        {
            int nextX = worm.X, nextY = worm.Y;
            var worms = world.Worms;
            
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