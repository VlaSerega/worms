namespace Worms
{
    public class Feild
    {
        private readonly int _height;
        private readonly int _width;

        public Feild(int height, int width)
        {
            _height = height;
            _width = width;
        }

        public int Height => _height;

        public int Width => _width;
    }
}