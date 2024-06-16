namespace Beacons_of_the_Elements
{
    public class Player
    {
        // Properties
        public int Health { get; set; } = 100;
        public int AttackPower { get; set; } = 30;
        public int Defense { get; set; } = 10;
        
        public bool IsDefending { get; private set; }
        public Location? CurrentLocation { get; private set; }


        public void Attack(Enemy enemy)
        {
            int damage = Math.Max(0, AttackPower - Defense);
            enemy.Health -= damage;
            Console.WriteLine($"You attack the {enemy.Name} for {damage} damage!");
            IsDefending = false;
        }


        public void Defend()
        {
            IsDefending = true;
            Console.WriteLine("You brace yourself for the next attack.");
        }


        public bool TryToFlee()
        {
            Random random = new Random();
            int fleeChance = 50;
            int roll = random.Next(100);
            return roll < fleeChance;
        }


        public void MoveTo(Location newLocation)
        {
            CurrentLocation = newLocation;
            Console.WriteLine($"\nYou are now in the {newLocation.Name}");
            Console.WriteLine(newLocation.Description);

            if (GameEngine.CheckForRandomEncounter())
            {
                Enemy enemy = GameEngine.GenerateRandomEnemy();
                GameEngine.StartCombat(this, enemy);
            }
        }
    }
}
