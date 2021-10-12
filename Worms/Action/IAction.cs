using System.Collections.Generic;
using Worms.GameModel;

namespace Worms.Action
{
    public interface IAction
    {
        void Execute(Worm worm, List<Worm> worms);
    }
}