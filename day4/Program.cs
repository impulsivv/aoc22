using System;
using System.Linq;

namespace day1
{
    class Program
    {
        static int contains(int leftlower, int leftupper, int rightlower, int rightupper)
        {
            if (leftlower <= rightlower && leftupper >= rightupper) return 1;
            else if (rightlower <= leftlower && rightupper >= leftupper) return 1;
            else return 0;
        }

        static int overlap(int leftlower, int leftupper, int rightlower, int rightupper)
        {
            IEnumerable<int> left = Enumerable.Range(leftlower, leftupper - leftlower + 1);
            IEnumerable<int> right = Enumerable.Range(rightlower, rightupper - rightlower + 1);
            IEnumerable<int> inter = left.Intersect(right);

            return inter.Any() ? 1 : 0;

        }
        static (int, int, int ,int) getBounds(string pair)
        {
            string[] elves = pair.Split(",");
            string[] left = elves[0].Split("-");
            string[] right = elves[1].Split("-");
            int leftlower = Int32.Parse(left[0]);
            int leftupper = Int32.Parse(left[1]);
            int rightlower = Int32.Parse(right[0]);
            int rightupper = Int32.Parse(right[1]);
            return (leftlower, leftupper, rightlower, rightupper);
        }
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] pairs = input.Split("\n");


            //part1
            Console.WriteLine(pairs.Select(pair =>
                {
                    (int leftlower, int leftupper, int rightlower, int rightupper) = getBounds(pair);
                    return contains(leftlower, leftupper, rightlower, rightupper);
                } ).Sum());

            //part2
            Console.WriteLine(pairs.Select(pair =>
                {
                    (int leftlower, int leftupper, int rightlower, int rightupper) = getBounds(pair);
                    return overlap(leftlower, leftupper, rightlower, rightupper);
                } ).Sum());

        }
    }
}
