using System;
using Worms.GameModel;

namespace Worms.Services
{
    public interface IFoodGenerator
    {
        Food GenerateFood();
    }
}