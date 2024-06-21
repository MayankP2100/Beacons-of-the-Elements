using System.ComponentModel;

namespace Beacons_of_the_Elements
{
    public class Player
    {
        // Properties
        public int Health { get; set; } = 50;
        public int AttackPower { get; set; } = 30;
        public int Defense { get; set; } = 10;
        public int Mana { get; set; } = 50;
        public bool IsAlive { get; set;} = true;
        public int Gold { get; set; } = 0;
        public int Experience { get; set; } = 0;

        public bool IsDefending { get; private set; }
        public Location? CurrentLocation { get; private set; }

        
        public void DisplayPlayerStats()
        {
            Console.WriteLine("Player stats:");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine($"Attack Power: {AttackPower}");
            Console.WriteLine($"Defense: {Defense}");
            Console.WriteLine($"Mana: {Mana}");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"Experience: {Experience}");
        }


        public void Attack(Enemy enemy)
        {
            bool isCritical = CheckForCriticalHit();
            int damage = AttackPower - Defense;
            if (isCritical)
            {
                damage *= 2;
                Console.WriteLine("Critical hit!");
            }
            damage = Math.Max(1, damage);
            enemy.Health -= damage;
            Console.WriteLine($"You attack the {enemy.Name} for {damage} damage!");
            IsDefending = false;
        }


        private bool CheckForCriticalHit()
        {
            Random random = new Random();
            int criticalChance = 10;
            int roll = random.Next(100);
            return roll < criticalChance;
        }


        public void UseSpecialAbility(Enemy enemy)
        {
            int abilityDamage = 20;
            enemy.Health -= abilityDamage;
            Console.WriteLine($"You use your special ability on the {enemy.Name} for {abilityDamage} damage!");
            Mana -= 15;
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


        public void ReceiveReward(Enemy enemy)
        {
            Gold += enemy.GoldDrop;
            Experience += enemy.ExperienceDrop;
            Console.WriteLine($"You receive {enemy.GoldDrop} gold and {enemy.ExperienceDrop} experience!");
            Mana = 50;
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
