using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAdventure
{
    public class Player : ICloneable
    {
        public int health, maxHealth, level, attack, defence, baseHealth, baseAttack, baseDefence, experience, nextLevelExperience;
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

        public void ChangeExperience(int amount)
        {
            experience += amount;

            if (experience >= nextLevelExperience)
                LevelUp();
        }

        public void LevelUp()
        {
            level++;
            Game.instance.AddToDisplay(String.Format("You have reached level {0}", level));
            CalculateNextExperience();
            calculateStats();

            if (experience >= nextLevelExperience)
            {
                LevelUp();
            }
        }

        public void CalculateNextExperience() // ((level^3) + ((level / 3)^3)) * 10
        {
            nextLevelExperience = ((int)((Math.Pow(level, 3)) + (Math.Pow((level / 3), 3))) * 10) + 73;
        }
    }
}
