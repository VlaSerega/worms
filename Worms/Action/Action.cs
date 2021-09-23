using System.Collections.Generic;
using Worms.GameModel;

namespace Worms.Action
{
    public interface Action
    {
        void Execute(Worm worm, List<Worm> worms);
    }
}