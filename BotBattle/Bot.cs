using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotBattle
{
    public class Bot
    {
        public string designation, name;
        public int hp, attack, defense, agility, speed;
        public Bot(string designation, string name, int hp, int attack, int defense, int agility, int speed)
        {
            this.designation = designation; //Bot number designating the order of creation of bots
            this.name = name;    //Randomized personal name of the competitor
            this.hp = hp;  //Total health points, implying how much damage a bot can incur before defeat. When empty, the bot has lost the battle
            this.attack = attack; //The damage that an attack will do to an opponent with 0 defense points
            this.defense = defense; //Body resistance to attack damage. 50% of this number is subtracted from attack damage
            this.agility = agility; //The probability of evading attacks and managing to strike.
            this.speed = speed; //how many seconds between moves. Lower values mean a faster bot
        }
    }
}