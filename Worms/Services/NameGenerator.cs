using System;

namespace Worms.Services
{
    public class NameGenerator : INameGenerator
    {
        public string NextName()
        {
            return Guid.NewGuid().ToString();
        }
    }
}