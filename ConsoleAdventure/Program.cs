using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
        }
    }

    class Game
    {
        public List<Monster> monsterTypes = new List<Monster>();
        public List<string> currentDisplay = new List<string>();

        // TODO - FIND BETTER WAY OF DOING THIS!
        public Location town = new Location();
        public Location inn = new Location();
        public Location shop = new Location();
        public Location forest = new Location();

        public Player player;

        public Random random = new Random();

        public Game()
        {

            Start();

            ReadLine();
        }

        public void AddLocations()
        {
            town.name = "Town";
            town.locations = new List<Location>()
            {
                inn, shop, forest
            };
            town.enterText = new List<string>()
            {
                String.Format("Welcome {0}, to the town", player.name),
                String.Format("We have an Inn, Shop and a path to the Forest")
            };

            inn.name = "Inn";
            inn.locations = new List<Location>()
            {
                town
            };
            inn.enterText = new List<string>()
            {
                String.Format("Welcome {0}, to the Inn", player.name),
                String.Format("We have a spare room you can use to recover your health")
            };

            shop.name = "Shop";
            shop.locations = new List<Location>()
            {
                town
            };
            shop.enterText = new List<string>()
            {
                String.Format("Welcome {0}, to the shop", player.name),
                String.Format("Feel free to browse our wares")
            };

            forest.name = "Forest";
            forest.hasCombat = true;
            forest.locations = new List<Location>()
            {
                town, forest
            };
            forest.enterText = new List<string>()
            {
                String.Format("You have entered the forest"),
                String.Format("BEWARE! Some dangers creatures lurk in these woods")
            };

        }

        public void AddMonsters()
        {
            monsterTypes.Add(new Monster("Turtle", forest)
            {
                baseHealth = 5,
                baseAttack = 2,
                baseDefence = 10,
                healthMultiplier = 0.6,
                attackMultiplier = 0.4,
                defenceMultiplier = 1.4
            });

            monsterTypes.Add(new Monster("Wolf", forest)
            {
                baseHealth = 4,
                baseAttack = 7,
                baseDefence = 3,
                healthMultiplier = 0.4,
                attackMultiplier = 1.6,
                defenceMultiplier = 0.6
            });

            monsterTypes.Add(new Monster("Deer", forest)
            {
                baseHealth = 2,
                baseAttack = 2,
                baseDefence = 2,
                healthMultiplier = 0.4,
                attackMultiplier = 0.4,
                defenceMultiplier = 0.4
            });

            monsterTypes.Add(new Monster("Man", null)
            {
                baseHealth = 5,
                baseAttack = 5,
                baseDefence = 3,
                healthMultiplier = 0.6,
                attackMultiplier = 0.6,
                defenceMultiplier = 0.6
            });
        }

        public void Start()
        {
            player = new Player();
            player.name = ReadLine("What is your name? ");
            player.level = 100;
            player.baseHealth = 10;
            player.baseAttack = 5;
            player.baseDefence = 4;
            player.healthMultiplier = 0.6;
            player.attackMultiplier = 0.5;
            player.defenceMultiplier = 0.5;
            player.calculateStats();

            AddLocations();
            AddMonsters();
            Goto(town, true);
        }

        public Monster GetMonster(Location currentLocation)
        {
            List<Monster> viableMonsters = new List<Monster>();
            foreach (Monster m in monsterTypes)
            {
                if (m.location == currentLocation || m.location == null)
                {
                    viableMonsters.Add((Monster)m.Clone());
                }
            }

            Monster monster = viableMonsters[random.Next(0, viableMonsters.Count)];

            monster.level = random.Next(player.level - 5, player.level + 5);
            if (monster.level < 1) monster.level = 1;

            monster.calculateStats();

            return monster;
        }

        public void Goto(Location location, bool doCombatCheck)
        {
            Clear();
            player.Goto(location);
            Monster monster = null;
            bool inCombat = false;

            for (int i = 0; i < location.enterText.Count; i++)
            {
                AddToDisplay((location.enterText[i]));
            }

            StringBuilder locations = new StringBuilder();
            for (int i = 0; i < location.locations.Count; i++)
            {
                locations.Append(String.Format("{0}. {1}   ", i + 1, location.locations[i].name));
            }
            AddToDisplay(locations.ToString());
            AddToDisplay("\n");

            if (location.hasCombat && doCombatCheck)
            {
                if (random.Next(0, 2) == 1)
                {
                    Thread.Sleep(400);
                    Clear();
                    monster = GetMonster(location);
                    inCombat = true;
                }
            }

            try
            {
                if (!inCombat)
                    Goto(location.locations[Convert.ToInt32(ReadLine("Where would you like to go? ")) - 1], true);
                else
                {
                    DoCombat(monster);
                }
            }
            catch (Exception)
            {
                PrintLine("You have typed in an incorrect destination.");
                Thread.Sleep(1500);
                Goto(location, false);
            }


        }

        void DoCombat(Monster monster)
        {
            while (monster.health > 0)
            {
                Clear();
                AddToDisplay(String.Format("A Level {0} {1}, has appeared in the {2}", monster.level, monster.name, player.location.name));
                AddToDisplay(String.Format("Health: {0}/{1}    Attack: {2}    Defence: {3}", monster.health, monster.maxHealth, monster.attack, monster.defence));
                AddToDisplay("");
                AddToDisplay("");
                AddToDisplay("");
                AddToDisplay("");
                AddToDisplay("");
                AddToDisplay("");
                AddToDisplay("");
                AddToDisplay("");
                AddToDisplay("");
                AddToDisplay("");
                AddToDisplay(String.Format("Health: {0}/{1}    Attack: {2}    Defence: {3}", player.health, player.maxHealth, player.attack, player.defence));
                AddToDisplay("1. Attack     2. Defend      3. Abilities/Spells     4. Items");

                string input = ReadLine("What would you like to do? ");

                switch (input)
                {
                    case ("1"):
                        if (player.attack < monster.defence / 2)
                            monster.health -= 1;
                        else
                            monster.health -= (player.attack - monster.defence / 2);
                        break;
                    default:
                        break;
                }

                if (monster.attack < player.defence / 2)
                    player.health -= 1;
                else
                    player.health -= (monster.attack - player.defence / 2);
            }

            Clear();
            AddToDisplay(String.Format("You have killed a level {0} {1} and gained 12 exp", monster.level, monster.name));
            Thread.Sleep(500);
            Goto(player.location, false);
        }

        void AddToDisplay(string text)
        {
            currentDisplay.Add(text);
            ClearD();
            ShowDisplay();
        }

        void ShowDisplay()
        {
            for (int i = 0; i < currentDisplay.Count; i++)
            {
                PrintLine(currentDisplay[i]);
            }
        }

        #region Util Methods

        void Print(string text)
        {
            Console.Write(text);
        }

        void PrintLine(string text)
        {
            Console.WriteLine(text);
        }

        string ReadLine()
        {
            return Console.ReadLine();
        }

        string ReadLine(string text)
        {
            Print(text);
            return Console.ReadLine();
        }

        void Clear()
        {
            Console.Clear();
            currentDisplay = new List<string>();
        }

        void ClearD()
        {
            Console.Clear();
        }

        #endregion
    }
}
