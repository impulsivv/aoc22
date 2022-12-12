using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace day1
{
    public class Monkey
    {
        public Queue<long> items { get; set; }
        public string[] op { get; set; }
        public long test { get; set; }
        public long testTrue { get; set; } 
        public long testFalse { get; set; }
        public long sawItems { get; set; }
        public long fuckme {get; set; }

        
        public Monkey(long[] items, string op, long test, long testTrue, long testFalse)
        {
            this.items = new Queue<long>();
            foreach(long item in items) this.items.Enqueue(item);
            this.op = op.Split(" ").ToArray();
            this.test = test;
            this.testTrue = testTrue;
            this.testFalse = testFalse;
            this.sawItems = 0;
            this.fuckme = 1;
        }

        public override string ToString()
        {
            return ("Monkey: " + items + " " + string.Join("",op) + " " + test + " "+ testTrue + " " + testFalse);
        }

        public (long what, long to) doStep()
        {
            long item = this.items.Dequeue();
            this.sawItems += 1;
            long to = 0;
            switch (op[0]) // operation * or +
            {
                case "*":
                    if(op[1] == "old") item *= item;
                    else item *= Int32.Parse(op[1]);
                    break;

                case "+":
                    if(op[1] == "old") item += item;
                    else item += Int32.Parse(op[1]);
                    break;

                default: break;
            }

            //item /= 3; //monkey bored part1
            item = item % fuckme;

            if(item % test == 0) to = testTrue;
            else to = testFalse;

            return (item, to);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] allMonkeys = input.Split("\n\n");
            Dictionary<long, Monkey> monkeys = new Dictionary<long, Monkey>();

            string regexInt = @"\b(?<!\.)\d+(?!\.)\b";
            Regex rg = new Regex(regexInt);
            MatchCollection intMatches =  rg.Matches("54, 65, 75, 74");
            long[] ints = intMatches.Select(x => long.Parse(x.Value)).ToArray();

            foreach(string monkey in allMonkeys)
            {
                string[] monkeyArr = monkey.Split("\n");
                long monkeyId = rg.Matches(monkeyArr[0]).Select(x => long.Parse(x.Value)).First();
                long[] items = rg.Matches(monkeyArr[1]).Select(x => long.Parse(x.Value)).ToArray();
                string op = string.Join(" ", monkeyArr[2].Split(" ").Skip(6).Take(2));
                long test = rg.Matches(monkeyArr[3]).Select(x => long.Parse(x.Value)).First();
                long testTrue = rg.Matches(monkeyArr[4]).Select(x => long.Parse(x.Value)).First();
                long testFalse = rg.Matches(monkeyArr[5]).Select(x => long.Parse(x.Value)).First();

                monkeys.Add(monkeyId, new Monkey(items, op, test, testTrue, testFalse));
            }
            
            //for part2 
            long fuckme = monkeys.Select(x => x.Value.test).Aggregate(1, (long accu, long next) => accu * next);
            foreach ((long id, Monkey monkey) in monkeys)
            {
                monkey.fuckme = fuckme;
            }

            // 20 part1, 10000 part2
            for(long j=0; j<10000; j++)
            {
                //1 round
                foreach((long id, Monkey monkey) in monkeys)
                {
                    long countItems = monkey.items.Count();
                    for(long i=0; i < countItems; i++)
                    {
                        (long what, long to) = monkey.doStep();
                        //Console.WriteLine(what + "->" + to);
                        monkeys[to].items.Enqueue(what);
                    }
                    //Console.WriteLine(monkey.ToString());
                }
            }


            var result = monkeys.OrderByDescending(x => x.Value.sawItems).Take(2).Select(x => x.Value.sawItems).Aggregate(1, (long accu, long next) => accu * next);
            Console.WriteLine(result);
        }
    }
}

