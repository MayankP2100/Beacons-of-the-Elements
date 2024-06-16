namespace Beacons_of_the_Elements
{
    public class GameEngine
    {
        // Properties
        private Player player;
        private World world;

        // Constructor
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

            while (player.IsAlive)
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


        public static void StartCombat(Player player, Enemy enemy)
        {
            Console.WriteLine($"You encounter a {enemy.Name}!");

            while (player.Health > 0 && enemy.Health > 0)
            {
                Console.WriteLine("\nWhat would you like to do?");
                Console.WriteLine("1. Attack\n2. Defend\n3. Flee\n4. Use Ability");
                string? input = Console.ReadLine();
                bool validInput = false;

                switch(input)
                {
                    case "1":
                        player.Attack(enemy);
                        validInput = true;
                        break;
                    case "2":
                        player.Defend();
                        validInput = true;
                        break;
                    case "3":
                        if (player.TryToFlee())
                        {
                            Console.WriteLine("You successfully fled from the enemy!");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("You failed to flee from the enemy!");
                        }
                        validInput = true;
                        break;
                    case "4":
                        if (player.Mana < 15)
                        {
                            Console.WriteLine("You do not have enough mana to use your special ability!");
                            validInput = false;
                            break;
                        }
                        player.UseSpecialAbility(enemy);
                        validInput = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        validInput = false;
                        break;
                }

                if (enemy.Health > 0 && validInput)
                {
                    enemy.Attack(player);
                }

                if (enemy.Health <= 0)
                {
                    Console.WriteLine($"You defeated the {enemy.Name}!");
                    return;
                }
                else if (player.Health <= 0)
                {
                    Console.WriteLine("You have been defeated!");
                    End(player);
                }
            }
        }


        private static void End(Player player)
        {
            Console.WriteLine("\nGame over!");
            player.IsAlive = false;
        }


        public static bool CheckForRandomEncounter()
        {
            Random random = new Random();
            int encounterChance = 20;
            int roll = random.Next(100);
            return roll < encounterChance;
        }


        public static Enemy GenerateRandomEnemy()
        {
            Random random = new Random();
            string[] enemyNames = { "Goblin", "Orc", "Troll", "Dragon" };
            string name = enemyNames[random.Next(enemyNames.Length)];
            int health = random.Next(50, 101);
            int attackPower = random.Next(10, 21);
            int defense = random.Next(5, 16);
            return new Enemy
            {
                Name = name,
                Health = health,
                AttackPower = attackPower,
                Defense = defense
            };
        }
    }
}
