using System;

namespace Worms
{
    public class MoveWormException : Exception
    {
        public MoveWormException(string message) : base(message)
        {
        }
    }
}