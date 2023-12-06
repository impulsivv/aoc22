using System;
using System.Linq;

namespace day1
{
    class Program
    {
        static bool checkSlice(string slice, int length)
        {
            //if distinct.length is 4, then all chars are distinct
            return ((String.Join("",slice.Distinct()).Length) == length) ? true : false;
        }
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            
            //part1
            Console.WriteLine(input);
            int j1 = 4;
            for(int i = 0; i <= input.Length - j1; i++)
            {
                string slice = input.Substring(i,j1);
                if (checkSlice(slice, j1))
                {
                    Console.WriteLine(i+j1);
                    break;
                } 
            }

            //part2
            int j2 = 14;
            for(int i = 0; i <= input.Length - j2; i++)
            {
                string slice = input.Substring(i,j2);
                if (checkSlice(slice, j2))
                {
                    Console.WriteLine(i+j2);
                    break;
                } 

            }
        }
    }
}
