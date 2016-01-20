using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextBasedAdventure
{
    public class Monster : ICloneable
    {
        public int health, maxHealth, level, attack, defence, baseHealth, baseAttack, baseDefence;
        public double healthMultiplier, attackMultiplier, defenceMultiplier;
        public string name;
        public Location location;

        public Monster(string name, Location location)
        {
            this.name = name;
            this.location = location;
        }

        public void calculateStats()
        {
            maxHealth = (int)(baseHealth + (level  * healthMultiplier));
            attack = (int)(baseAttack + (level * attackMultiplier));
            defence = (int)(baseDefence + (level * defenceMultiplier));

            health = maxHealth;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
