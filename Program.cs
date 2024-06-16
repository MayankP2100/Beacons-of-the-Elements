using System.Data.Common;
using System.Runtime.CompilerServices;

namespace Beacons_of_the_Elements
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameEngine game = new GameEngine();
            game.Start();
        }
    }
    
    public class World
    {
        // Properties
        public Location Forest { get; private set; }
        public Location Cave { get; private set; }
        public Location Village { get; private set; }

        // Constructor
        public World()
        {
            Forest = new Location("Forest", "A dark and mysterious forest");
            Cave = new Location("Cave", "A dark and damp cave");
            Village = new Location("Village", "A small village");
        }
    }

    public class Location
    {
        // Properties
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Location(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
