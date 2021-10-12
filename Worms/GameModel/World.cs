using System.Collections.Generic;

namespace Worms.GameModel
{
    public class World
    {
        private readonly List<Worm> _worms;
        private readonly List<Food> _foods;
        private readonly int _seed;

        private int _moveNumber;

        public World(List<Worm> worms = null, List<Food> foods = null, int seed = 0)
        {
            _worms = worms ?? new List<Worm>();
            _foods = foods ?? new List<Food>();
            _seed = seed;
            _moveNumber = 0;
        }

        public void AddWorm(Worm worm)
        {
            _worms.Add(worm);
        }

        public void AddFood(Food food)
        {
            _foods.Add(food);
        }

        public void IncreaseMoveNumber()
        {
            _moveNumber++;
        }

        public List<Worm> Worms => _worms;

        public List<Food> Foods => _foods;

        public int MoveNumber => _moveNumber;

        public int Seed => _seed;
    }
}