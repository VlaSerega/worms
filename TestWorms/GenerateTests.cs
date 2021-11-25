using System.Collections.Generic;
using NUnit.Framework;
using Worms.GameModel;
using Worms.Services;

namespace TestWorms
{
    [TestFixture]
    public class GenerateTests
    {
        [Test]
        public void GenerateFoodTest()
        {
            FoodGenerator foodGenerator = new FoodGenerator();
            List<Food> foods = new List<Food>();

            Food newFood = foodGenerator.GenerateFood(foods);
            foods.Add(newFood);

            newFood = foodGenerator.GenerateFood(foods);

            Assert.Null(foods.FindLast(food => food.X == newFood.X && food.Y == newFood.Y));
        }

        [Test]
        public void GenerateNameTest()
        {
            NameGenerator nameGenerator = new NameGenerator();

            Assert.AreNotEqual(nameGenerator.NextName(), nameGenerator.NextName());
        }
    }
}