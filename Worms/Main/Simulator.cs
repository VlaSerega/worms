using System;
using System.Collections.Generic;
using Worms.GameModel;

namespace Worms.Main
{
    public class Simulator
    {
        private readonly World _world;

        /// <summary>
        /// Создает новый симулятор.
        /// </summary>
        public Simulator()
        {
            _world = new World();
            _world.AddWorm(new Worm("Федор", 0, 0));
        }

        /// <summary>
        /// Запускает симуляцию червяков.
        /// </summary>
        public void Run()
        {
            while (_world.MoveNumber != Const.MaxMoveNumber)
            {
                // Если еда сгенерировалась на червяке
                foreach (var worm in _world.Worms)
                {
                    StayOnFood(worm);
                }

                //Червяки ходят
                MakeWormsStep();

                // Еда тухнет
                foreach (var food in _world.Foods)
                {
                    food.ReduceHealth();
                    Console.WriteLine(food);
                }

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
            // Хорошо ли это с точки зрения ООП, что создает нового червяка выполнение метода у Action Execute,
            // а здоровье вычитается у червяка через метод Eat
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
        /// <param name="worm">Проверяемый червяк.</param>
        private void StayOnFood(Worm worm)
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
}