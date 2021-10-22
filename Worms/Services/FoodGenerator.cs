using System;
using Worms.GameModel;
using Worms.Main;

namespace Worms.Services
{
    public class FoodGenerator : IFoodGenerator
    {
        private Random _random;

        public FoodGenerator()
        {
            _random = new Random(Const.Seed);
        }

        public Food GenerateFood()
        {
            return new Food(_random.NextNormal(Const.MU, Const.Sigma), _random.NextNormal(Const.MU, Const.Sigma));
        }
    }
}