using Worms.GameModel;
using Worms.Action;

namespace Worms.Services
{
    public interface IBehavior
    {
        IAction GetAction(Worm worm, WorldState state);
    }
}