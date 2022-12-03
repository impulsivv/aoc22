using System;
using System.Linq;

namespace day1
{
    class Program
    {
        static char commonChar(string left, string right)
        {
            for (int i = 0; i < left.Length; i++)
                for (int j = 0; j < right.Length; j++)
                    if (left[i] == right[j]) return left[i];
            return ' ';
        }

        static string commonString(string left, string right)
        {
            HashSet<char> commons = new HashSet<char>();
            for (int i = 0; i < left.Length; i++)
                for (int j = 0; j < right.Length; j++)
                    if (left[i] == right[j]) commons.Add(left[i]);
            return string.Join("",commons);
        }

        static char commonCharWithThree(string first, string second, string third)
        {
            var ab = commonString(first, second);
            var bc = commonString(second, third);
            var ca = commonString(third, first);

            return char.Parse(commonString(ab, commonString(bc, ca)));
        }
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] bags = input.Split("\n");

            //part1
            var commons = 
            bags.Select(x =>
                {
                    var (left, right) = (x.Substring(0, x.Length / 2), x.Substring( x.Length / 2));
                    char common = commonChar(left, right);
                    return common;
                }).Select(c => char.IsLower(c) ? (int)c - 97 + 1 : (int)c - 65 + 27 );
            
            Console.WriteLine(commons.Sum());


            //part2
            List<int> prios = new List<int>();
            for (int i=0; i< bags.Length; i++)
            {
                if(i % 3 == 2)
                {
                    char prio = commonCharWithThree(bags[i-2], bags[i-1], bags[i]);
                    prios.Add(char.IsLower(prio) ? (int)prio - 97 + 1 : (int)prio - 65 + 27 ); 
                }
            }
            Console.WriteLine(prios.Sum());
    
        }
    }
}
