using System;
using System.Collections.Generic;
using Worms.GameModel;

namespace Worms.Services
{
    public interface IFoodGenerator
    {
        Food GenerateFood(List<Food> foods);
    }
}