namespace Beacons_of_the_Elements
{
    public class Enemy
    {
        // Properties
        public required string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }
        public int Defense { get; set; }


        public void Attack(Player player)
        {
            int damage = Math.Max(0, AttackPower - player.Defense);
            
            if (player.IsDefending)
            {
                damage = Math.Max(0, damage / 2);
            }
            player.Health -= damage;
            Console.WriteLine($"The {Name} attacks you for {damage} damage!");
        }
    }
}