using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Worms.GameModel;

namespace Worms.Services
{
    public class Simulator : IHostedService
    {
        private readonly World _world;
        private readonly IFoodGenerator _foodGenerator;

        /// <summary>
        /// Create simulator. Initial game with one Worm.
        /// <param name="generator">Food generator for World</param>
        /// </summary>
        public Simulator(IFoodGenerator generator)
        {
            _world = new World();
            _world.AddWorm(new Worm("Федор", 0, 0));
            _foodGenerator = generator;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Run simulation of World.
        /// </summary>
        public void Run()
        {
            while (_world.MoveNumber != Const.MaxMoveNumber)
            {
                // Generate food
                GenerateFood();

                // Check that food was generated on a worm
                StayOnFood();

                MakeWormsStep();

                // Food spoils
                foreach (var food in _world.Foods)
                {
                    food.ReduceHealth();
                    Console.WriteLine(food);
                }

                // Check that worms stand on foods
                StayOnFood();

                // Remove spoiled foods and dead worms
                _world.Worms.RemoveAll(worm => worm.Health == 0);
                _world.Foods.RemoveAll(food => food.Health == 0);

                // Increase move counter
                _world.IncreaseMoveNumber();
            }
        }

        /// <summary>
        /// Make worms steps.
        /// </summary>
        private void MakeWormsStep()
        {
            var copyWorms = new List<Worm>(_world.Worms);

            foreach (var worm in copyWorms)
            {
                // Ask worm about next action
                var action = worm.ChooseAction(_world);

                // If action is valid, then make it
                try
                {
                    action.Execute(worm, _world.Worms);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                worm.ReduceHealth();
                Console.WriteLine(worm);
            }
        }

        /// <summary>
        /// Check that worm stand on food. If yes, then it eat. 
        /// </summary>
        private void StayOnFood()
        {
            foreach (var worm in _world.Worms)
            {
                Food eatenFood = null;

                foreach (var food in _world.Foods)
                {
                    // If the worm got up on food, then it eats 
                    if (food.X == worm.X && food.Y == worm.Y)
                    {
                        worm.Eat();
                        // Remembering the food we eat
                        eatenFood = food;
                    }
                }

                if (eatenFood != null)
                {
                    _world.Foods.Remove(eatenFood);
                }
            }
        }

        private void GenerateFood()
        {
            Food newFood;

            do
            {
                newFood = _foodGenerator.GenerateFood();
            } while (_world.Foods.FindLast(food => food.X == newFood.X && food.Y == newFood.Y) != null);

            _world.AddFood(newFood);
        }
    }
}