using System;
using System.Linq;
using System.Collections.Generic;

namespace day1
{
    class Program
    {
        static void Main(string[] args)
        {
            //TODO: do that with linq you lazy fuck
            List<int> ints = new List<int>();
            string[] lines = System.IO.File.ReadAllLines(@"./input.txt");//.Select(line => Convert.ToInt32(line)).ToList<int>();
            int val = 0;

            foreach(string line in lines)
            {
                if (Int32.TryParse(line, out var temp)) val += Int32.Parse(line);
                else
                {
                    ints.Add(val);
                    val = 0;
                } 
            }

            //part1
            Console.WriteLine(ints.Max());

            //part2
            List<int> ints2 = ints.OrderBy(p => p).Reverse().Take(3).ToList<int>();
            Console.WriteLine(ints2.Sum());
            //ints2.ForEach(x => Console.WriteLine(x));
            
        }
    }
}
