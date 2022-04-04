//A simulation of two bots battling one another. The bots can be named by the user or a randomized name will
//be given. The bots will have various stats like hp, attack, defense, speed, and agility that determine the
//outcome of the battle.

//hp is the health points governing weather the bot is still alive
//attack determines the damage effect. Higher attack increases damage
//defense determines the ability to withstand an attack. Higher defense reduces damage
//speed determines how much time between attacks
//agility determines the chances of a bot evading an attack

using System;
using System.Threading;
using System.Collections.Generic;

namespace BotBattle
{
    public class Program
    {
        public static string red_status;
        public static string blue_status;
        public static void sleep(int seconds)
        {
            Thread.Sleep(seconds*1000);
            return;
        }
        public static Dictionary<Bot, Bot> tournamentCompile(Bot[] bots)
        {
            Dictionary<Bot, Bot> next_round = new Dictionary<Bot, Bot>();
            for(int i = 0; i < bots.Length; i += 2)
            {
                Bot red = bots[i];
                Bot blue = bots[i + 1];
                next_round.Add(red, blue);
            }
            return next_round;
        }
        public static void runBot(Bot self, Bot opponent, string side)
        {
            Random rnd = new Random();
            int attack_damage = self.attack - (opponent.defense / 2);
            while (self.hp > 0)
            {
                if (rnd.Next(1, 100) > (self.agility+opponent.agility)/2) {
                    opponent.hp = opponent.hp - attack_damage;
                    Console.WriteLine($"{self.name} has successfully dealt an attack on {opponent.name}.");
                }
                else
                {
                    Console.WriteLine($"{opponent.name} dodged an attack launched by {self.name}.");
                }
                sleep(self.speed);
                if(opponent.hp <= 0)
                {
                    if (side == "red")
                    {
                        red_status = "win";
                        blue_status = "lose";
                    }
                    else
                    {
                        red_status = "lose";
                        blue_status = "win";
                    }
                    return;
                }
            }
            return;
        }
        public static void battleBots(Bot red, Bot blue)
        {
            Console.WriteLine($"{red.name} vs {blue.name}");
            Console.Write("ENTER to watch the match.");
            Console.ReadLine();
            Thread ra = new Thread(() => runBot(red, blue, "red"));
            Thread ba = new Thread(() => runBot(blue, red, "blue"));
            ra.Start();
            ba.Start();
            ra.Join();
            ba.Join();
            if(red_status == "win")
            {
                Bot winner = red;
                Console.WriteLine($"Winner:\n  Red Team    {red.designation}   {red.name}");
            }
            else
            {
                Bot winner = blue;
                Console.WriteLine($"Winner:\n  Blue Team   {blue.designation}   {blue.name}");
            }
            return;
        }
        public static void Main(string[] arg)
        {
            RandomName rn = new RandomName();
            Random rnd = new Random();

            Bot[] bots = { new Bot("Bot 1"/*designation*/, rn.generateName()/*name*/, rnd.Next(100, 200)/*hp*/, rnd.Next(10, 20)/*attack*/, rnd.Next(5,10)/*defense*/, rnd.Next(10, 50)/*agility*/, rnd.Next(1,5)/*speed*/), new Bot("Bot 2", rn.generateName(), rnd.Next(100, 200), rnd.Next(10, 20), rnd.Next(5, 10), rnd.Next(10, 50), rnd.Next(1, 5)) };
            var count = bots.Count();
            if (count % 2 != 0)
            {
                throw new Exception("The number of competitors must be even in order to design a tournament");
            }
            else
            {
                Console.WriteLine("The number of competitors is even. Compiling the first stage of the tournament now...");
            }
            //A while loop shall go here for the tournament loop:   while(bots.Count!=1){//code to execute each tournament round}
            var round_setup = tournamentCompile(bots);  //compiles the pairing for each tournament round
            foreach (Bot bot in round_setup.Keys)
            {
                Bot red = bot;
                Bot blue = round_setup[bot];
                battleBots(red, blue);
            }
        }
    }
}