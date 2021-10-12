using System;
using System.Collections.Generic;
using Worms.GameModel;

namespace Worms.Main
{
    public class Simulator
    {
        private readonly World _world;
        private readonly Random _random;

        /// <summary>
        /// Создает новый симулятор.
        /// </summary>
        public Simulator()
        {
            _world = new World();
            _world.AddWorm(new Worm("Федор", 0, 0));
            _random = new Random(_world.Seed);
        }

        /// <summary>
        /// Запускает симуляцию червяков.
        /// </summary>
        public void Run()
        {
            while (_world.MoveNumber != Const.MaxMoveNumber)
            {
                //Генерация еды
                GenerateFood();

                // Если еда сгенерировалась на червяке
                StayOnFood();

                // Червяки ходят
                MakeWormsStep();

                // Еда тухнет
                foreach (var food in _world.Foods)
                {
                    food.ReduceHealth();
                    Console.WriteLine(food);
                }

                // Если червяк встал на еду
                StayOnFood();

                // Удаляем протухшую еду и мертвых червяков
                _world.Worms.RemoveAll(worm => worm.Health == 0);
                _world.Foods.RemoveAll(food => food.Health == 0);

                // Счетчик ходов увеличиваем
                _world.IncreaseMoveNumber();
            }
        }

        /// <summary>
        /// Совершает ход червяков.
        /// </summary>
        private void MakeWormsStep()
        {
            var copyWorms = new List<Worm>(_world.Worms);

            foreach (var worm in copyWorms)
            {
                // Спрашиваем, что хочет сделать червяк.
                var action = worm.ChooseAction(_world);

                // Если действие допустимо
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
        /// Проверяет, что червяк <code>worm</code> стоит на еде. Если стоит на еде, то червяк съедает еду. 
        /// </summary>
        private void StayOnFood()
        {
            foreach (var worm in _world.Worms)
            {
                // Съеденная еда
                Food eatenFood = null;

                foreach (var food in _world.Foods)
                {
                    // Если червяк worm, встал на еду food
                    if (food.X == worm.X && food.Y == worm.Y)
                    {
                        // Едим
                        worm.Eat();
                        // Запоминаем, что съели
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
            int x, y;

            do
            {
                x = _random.NextNormal(Const.MU, Const.Sigma);
                y = _random.NextNormal(Const.MU, Const.Sigma);
            } while (_world.Foods.FindLast(food => food.X == x && food.Y == y) != null);

            _world.AddFood(new Food(x, y));
        }
    }
}