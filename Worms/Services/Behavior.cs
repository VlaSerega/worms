using System;
using Worms.Action;
using Worms.GameModel;

namespace Worms.Services
{
    public class Behavior : IBehavior
    {
        public IAction GetAction(Worm worm, WorldState state)
        {
            Food chosenFood = null;
            int x = worm.X;
            int y = worm.Y;
            
            foreach (var food in state.Foods)
            {
                if (chosenFood == null || Math.Abs(food.X - x) + Math.Abs(food.Y - y) <
                    Math.Abs(chosenFood.X - x) + Math.Abs(chosenFood.Y - y))
                {
                    chosenFood = food;
                }
            }

            IAction action = new ActionNothing();

            if (chosenFood != null)
            {
                if (chosenFood.X - x < 0)
                {
                    action = new ActionMove(Direction.Up);
                }

                if (chosenFood.X - x > 0)
                {
                    action = new ActionMove(Direction.Down);
                }

                if (chosenFood.Y - y < 0)
                {
                    action = new ActionMove(Direction.Left);
                }

                if (chosenFood.Y - y > 0)
                {
                    action = new ActionMove(Direction.Right);
                }
            }

            return action;
        }
    }
}