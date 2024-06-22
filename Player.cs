using System.ComponentModel;
using System.Numerics;

namespace Beacons_of_the_Elements
{
    public class Player
    {
        // Properties
        public int Health { get; set; } = 50;
        public int AttackDamage { get; set; } = 30;
        public int Power { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Mana { get; set; } = 50;
        public bool IsAlive { get; set;} = true;
        public int Gold { get; set; } = 0;
        public int Experience { get; set; } = 0;
        public int Level { get; set; } = 1;

        public bool IsDefending { get; private set; }
        public Location? CurrentLocation { get; private set; }

        private int rng = new Random().Next(0, 8);


        public void DisplayPlayerStats()
        {
            Console.WriteLine("Player stats:");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine($"AttackDamage: {Attack}");
            Console.WriteLine($"Power: {Power}");
            Console.WriteLine($"Defense: {Defense}");
            Console.WriteLine($"Mana: {Mana}");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"Experience: {Experience}");
        }


        public void Attack(Enemy enemy)
        {
            bool isCritical = CheckForCriticalHit();
            int baseDamage = AttackDamage +
                ((AttackDamage + Level) / 32) *
                ((AttackDamage * Level) / 32);
            int maxDamage = ((Power * (512 - Defense) * baseDamage) / (16 * 512));
            int actualDamage = maxDamage * rng * (3841 + rng) / 4096;

            if (isCritical)
            {
                actualDamage *= 2;
                Console.WriteLine("Critical hit!");
            }
            actualDamage = Math.Max(1, actualDamage);
            enemy.Health -= actualDamage;
            Console.WriteLine($"You attack the {enemy.Name} for {actualDamage} damage!");
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
