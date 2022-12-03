using System;
using System.Linq;

namespace day2
{
    class Program
    {
        static Dictionary<char, string> charToChoice = new Dictionary<char, string>()
            {
                { 'A', "R" },
                { 'B', "P" },
                { 'C', "S"},
                { 'X', "R" },
                { 'Y', "P" },
                { 'Z', "S"},
                { ' ', ""}
            };
        
            static Dictionary<string, int> charToPoints = new Dictionary<string, int>()
            {
                { "R", 1 },
                { "P", 2 },
                { "S", 3 }
            };
  
            static Dictionary<string, string> getWin = new Dictionary<string, string>()
            {
                { "R", "P" },
                { "P", "S" },
                { "S", "R" }
            };

            static Dictionary<string, string> getLose = new Dictionary<string, string>()
            {
                { "R", "S" },
                { "P", "R" },
                { "S", "P" }
            };
        static int gamerules(string enemy, string me)
        {
            //win 6, draw 3, loss 0
            //enemy A Rock, B Paper, C Scissor
            //me 2 Y Paper, 1 X Rock  ,3 Z Scissor
            if (enemy == me) return 3;
            
            if ((me=="R" && enemy=="S")
             || (me=="S" && enemy=="P")
             || (me=="P" && enemy=="R")) return 6;
             
            else return 0;
        }

        static int strategy(string enemy, string me, Dictionary<string, int> charToPoints,
                            Dictionary<string, string> getWin, Dictionary<string, string> getLose)
        {
            // me Rock = lose, Paper = draw, Scissor = win
            if(me=="R") return ((0 + charToPoints[getLose[enemy]]));

            else if(me=="P") return ((3 + charToPoints[enemy]));

            else return ((6 + charToPoints[getWin[enemy]]));    
        }

        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            var rounds = input.Split("\n").Select(x => x.Select(y => charToChoice[y]));

            //part1
            var points = 
            rounds.Select(round =>
                {
                var enemy = string.Join("",round.Take(1));
                var me = string.Join("",round.Skip(1).Take(2));
                return (charToPoints[me] + gamerules(enemy, me));
                }

            ).Sum();
            Console.WriteLine(points);

            //part2
            var points2 = 
            rounds.Select(round =>
                {
                var enemy = string.Join("", round.Take(1));
                var me = string.Join("", round.Skip(1).Take(2));
                return (strategy(enemy, me, charToPoints, getWin, getLose));
                }

            ).Sum();
            Console.WriteLine(points2);

        }
    }
}
