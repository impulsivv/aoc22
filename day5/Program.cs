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
        static void makeMove(string move, ref List<Stack<string>> fieldStack)
        {
            (int amount, int from, int to) = getMoveInfo(move);
            for(int i=0; i < amount; i++)
            {
                fieldStack[to].Push(fieldStack[from].Pop());
            }
        }

        static void makeMoveMultiple(string move, ref List<Stack<string>> fieldStack)
        {
            (int amount, int from, int to) = getMoveInfo(move);
            Stack<string> buffer = new Stack<string>();
            for(int i=0; i < amount; i++)
            {
                buffer.Push(fieldStack[from].Pop());
            }
            foreach(string item in buffer)
            {
                fieldStack[to].Push(item);
            }
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
 
            //part1
            
            List<Stack<string>> fieldStack = createField(itemGen, fieldCount);            
            foreach(string move in moves) makeMove(move, ref fieldStack);


            string result = string.Empty;
            foreach(Stack<string> stack in fieldStack) result += stack.Pop();

            Console.WriteLine(result.Replace(" ",string.Empty).Replace("[",string.Empty).Replace("]",string.Empty));
            
            //part2
            List<Stack<string>> fieldStack2 = createField(itemGen, fieldCount);            
            foreach(string move in moves) makeMoveMultiple(move, ref fieldStack2);


            string result2 = string.Empty;
            foreach(Stack<string> stack in fieldStack2) result2 += stack.Pop();

            Console.WriteLine(result2.Replace(" ",string.Empty).Replace("[",string.Empty).Replace("]",string.Empty));
        }
    }
}
