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
                    throw new ReproductionWormException(
                        $"Worm {worm.Name} can't reproduction {_direction.ToString()}"
                    );
            }

            worms.Add(new Worm(world.NameGenerator.NextName(), nextX, nextY, Const.StartHealthNewWorm));

            worm.Reproduction();
        }
    }
}