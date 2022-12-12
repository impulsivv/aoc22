using System;
using System.Linq;

namespace day1
{
    class Program
    {
        static int getValue(string[] values, Dictionary<string, string[]> folderStructure, int result)
        {
            foreach(string val in values)
            {
                //Console.WriteLine(val);
                if(val.StartsWith("dir"))
                {
                    string dir = val.Split(" ")[1];
                    result = getValue(folderStructure[dir], folderStructure, result);
                    
                }else
                {
                    int number = Int32.Parse(val.Split(" ")[0]);
                    result += number;
                    
                }
            }
            return result;   
        }

        static int getValue2(string[] values, Dictionary<string, string[]> folderStructure)
        {
            int result = 0;
            foreach(string val in values)
            {
                //Console.WriteLine(val);
                if(val.StartsWith("dir"))
                {
                    string dir = val.Split(" ")[1];
                    result += getValue2(folderStructure[dir], folderStructure);
                }else
                {
                    int number = Int32.Parse(val.Split(" ")[0]);
                    result += number;
                }
            }
            return result;
        }
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] commands = input.Split("$ ");
            string[] filteredCommands = commands.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            
            Dictionary<string, string[]> folderStructure = new Dictionary<string, string[]>();
            Stack<String> directoryStack = new Stack<string>();
            string dir = string.Empty;
            //part1
            foreach(string command in filteredCommands)
            {
                if (command.Contains("cd")) dir = command[3..].Replace("\n", string.Empty);
                else{ // ls
                    string[] clearCommand = command.Replace("ls\n", string.Empty).Split("\n").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                    switch(dir)
                    {
                        case "/" :
                            folderStructure.Add(dir, clearCommand);
                            directoryStack.Clear();
                            break;
                        case ".." :
                            dir = directoryStack.Pop();
                            break;
                        default: // dir w.e.NameItHaves
                            if(! folderStructure.ContainsKey(dir))
                            {
                                folderStructure.Add(dir, clearCommand);
                                directoryStack.Push(dir);
                            }
                            break;
                    }
                    //Console.WriteLine("HIER IST CD " + dir);
                }
                
            }


            foreach(var key in folderStructure)
            {
                int result = 0;
                //Console.WriteLine(key.Key + "-----");
                foreach(var we in key.Value) Console.WriteLine( key.Key + "|"+ we);
                //Console.WriteLine();
            }
            
            foreach(var key in folderStructure)
            {
                int result = 0;
                int endresult = 0;
                endresult += getValue2( key.Value, folderStructure);
                //Console.WriteLine(key.Key + "-----" + getValue( folderStructure[key.Key], folderStructure, result));
                Console.WriteLine(endresult);
                if(key.Key != "/")
                {
                       
                }

                //Console.WriteLine();
            }
            //part2
            //Console.WriteLine(we.OrderBy(p => p).Reverse().Take(3).Sum());
            
        }
    }
}
