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
        private readonly IFoodGenerator _foodFoodGenerator;
        private readonly IFileLogger _logger;
        private readonly IHostApplicationLifetime _lifetime;
        
        /// <summary>
        /// Create simulator. Initial game with one Worm.
        /// <param name="foodGenerator">Food generator for World</param>
        /// <param name="logger">Logger for World</param>
        /// <param name="nameGenerator">Name generator for Worms</param>
        /// </summary>
        public Simulator(IHostApplicationLifetime lifetime, IFoodGenerator foodGenerator, IFileLogger logger, INameGenerator nameGenerator)
        {
            _world = new World(nameGenerator);
            _world.AddWorm(new Worm(nameGenerator.NextName(), 0, 0));
            _foodFoodGenerator = foodGenerator;
            _logger = logger;
            _lifetime = lifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(Run);
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Run simulation of World.
        /// </summary>
        private void Run()
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
                }

                // Check that worms stand on foods
                StayOnFood();

                // Remove spoiled foods and dead worms
                _world.Worms.RemoveAll(worm => worm.Health == 0);
                _world.Foods.RemoveAll(food => food.Health == 0);

                // Increase move counter
                _world.IncreaseMoveNumber();
            }
            
            _lifetime.StopApplication();
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
                    action.Execute(worm, _world);
                    _logger.LogInfo("{0} made action {1}", worm, action);
                }
                catch (Exception e)
                {
                    _logger.LogInfo(e, "{0} try choose bad action {1}", worm, action);
                }

                worm.ReduceHealth();
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
                newFood = _foodFoodGenerator.GenerateFood();
            } while (_world.Foods.FindLast(food => food.X == newFood.X && food.Y == newFood.Y) != null);

            _world.AddFood(newFood);
            _logger.LogInfo("New food was generated {0}", newFood);
        }
    }
}