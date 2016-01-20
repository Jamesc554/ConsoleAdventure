using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    public class Player : ICloneable
    {
        public int health, maxHealth, level, attack, defence, baseHealth, baseAttack, baseDefence;
        public double healthMultiplier, attackMultiplier, defenceMultiplier;
        public string name;
        public Location location;

        public Player()
        {

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

        public void Goto(Location location)
        {
            this.location = location;
        }
    }
}
