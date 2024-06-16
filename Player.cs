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

        public bool IsDefending { get; private set; }
        public Location? CurrentLocation { get; private set; }


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
