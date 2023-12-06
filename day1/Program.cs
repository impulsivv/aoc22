using System;
using System.Linq;

namespace day1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] elves = input.Split("\n\n");

            var we = 
            elves.Select(x =>
                x.Split().Select(a =>
                    Int32.Parse(a)
                )
            ).Select(x => x.Sum());

            //part1
            Console.WriteLine(we.Max());

            //part2
            Console.WriteLine(we.OrderBy(p => p).Reverse().Take(3).Sum());
        }
    }
}
