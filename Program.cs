using System.Data.Common;

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

    public class GameEngine
    {
        private Player player;
        private World world;

        public GameEngine()
        {
            player = new Player();
            world = new World();
        }

        public void Start()
        {
            Console.WriteLine("Game started");
            Console.WriteLine("You find yourself in a village. Where would you like to go?");
            Console.WriteLine("1. The Forest\n2. The Cave\n3. Stay in the Village");

            while (true)
            {
                Console.WriteLine();
                string? input = Console.ReadLine();
                if (input == "1")
                {
                    player.MoveTo(world.Forest);
                }
                else if (input == "2")
                {
                    player.MoveTo(world.Cave);
                }
                else if (input == "3")
                {
                    player.MoveTo(world.Village);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }
    }

    public class Player
    {
        public Location? CurrentLocation { get; private set; }

        public void MoveTo(Location newLocation)
        {
            CurrentLocation = newLocation;
            Console.WriteLine($"\nYou are now in the {newLocation.Name}");
            Console.WriteLine(newLocation.Description);
        }
    }

    public class World
    {
        public Location Forest { get; private set; }
        public Location Cave { get; private set; }
        public Location Village { get; private set; }

        public World()
        {
            Forest = new Location("Forest", "A dark and mysterious forest");
            Cave = new Location("Cave", "A dark and damp cave");
            Village = new Location("Village", "A small village");
        }
    }

    public class Location
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Location(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
