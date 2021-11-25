using System;
using System.Collections.Generic;
using Worms.GameModel;

namespace Worms.Services
{
    public class FoodGenerator : IFoodGenerator
    {
        private readonly Random _random;

        public FoodGenerator()
        {
            _random = new Random(Const.Seed);
        }

        public Food GenerateFood(List<Food> foods)
        {
            Food newFood;

            do
            {
                newFood = new Food(
                    _random.NextNormal(Const.MU, Const.Sigma),
                    _random.NextNormal(Const.MU, Const.Sigma)
                );
            } while (foods.FindLast(food => food.X == newFood.X && food.Y == newFood.Y) != null);

            return newFood;
        }
    }
}