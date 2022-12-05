using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace day1
{
    class Program
    {
        static IEnumerable<string> splitString(string str, int n)
        {
            if (String.IsNullOrEmpty(str) || n < 1)
            {
                throw new ArgumentException();
            }

            return Enumerable.Range(0, str.Length / n)
                            .Select(i => str.Substring(i * n, n));
        }
        static List<Stack<string>> createField(IEnumerable<string> field, int fieldCount)
        {
            //TODO: makes this not so ugly, god may forgive me
            List<Stack<string>> fieldStack = new List<Stack<string>>();
            for(int i=0; i < fieldCount; i++)
            {
                fieldStack.Add(new Stack<string>());
            }
            int j = 0;
            foreach(string item in field)
            {
                if(j == fieldCount) j=0;

                if(!item.All(Char.IsWhiteSpace)) fieldStack[j].Push(item);
                j++;
            }
            return fieldStack;
        }

        static (int amount, int from, int to) getMoveInfo(string move)
        {
            // allways 3 numbers, so its hardcoded, for w.e. reason first element is emptystring ¯\_(ツ)_/¯
            // -1 because indexing is from 1 for some reason
            string[] numbers = Regex.Split(move, @"\D+");
            return (Int32.Parse(numbers[1]), Int32.Parse(numbers[2]) - 1, Int32.Parse(numbers[3]) - 1);
        }
        static string makeMove(string move, List<Stack<string>> fieldStack)
        {
            (int amount, int from, int to) = getMoveInfo(move);
            for(int i=0; i < amount; i++)
            {
                fieldStack[to].Push(fieldStack[from].Pop());
            }
            return move;
        }
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] fieldAndMoves = input.Split("\n\n");
            string field = String.Join("\n", fieldAndMoves[0].Split("\n").SkipLast(1).Reverse());
            int splitNumber = 4; // TODO: get information from field
            int fieldCount = (fieldAndMoves[0].Split("\n")[0].Length + 1) / splitNumber;

            string[] moves = fieldAndMoves[1].Split("\n");

            IEnumerable<string> itemGen = splitString(field, splitNumber);
            Console.WriteLine(String.Join("newline", itemGen));
            //part1
            Console.WriteLine(field);
            Console.WriteLine(moves);
            Console.WriteLine(fieldCount);

            List<Stack<string>> fieldStack = createField(itemGen, fieldCount);
            moves.Select(move => makeMove(move, fieldStack));
            Console.WriteLine("hehe");
            Console.WriteLine(fieldStack[0].Pop());
            Console.WriteLine(fieldStack[1].Pop());
            Console.WriteLine(fieldStack[2].Pop());
            

            //part2
            //Console.WriteLine(we.OrderBy(p => p).Reverse().Take(3).Sum());
        }
    }
}
