using System;
using System.Linq;

namespace day1
{
    class Program
    {
        static string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        static int getValue(string[] values, Dictionary<string, string[]> folderStructure, int result)
        {
            foreach(string val in values){
                //Console.WriteLine(val);
                if(val.StartsWith("dir")){
                    string dir = val.Split(" ")[1];
                    result += getValue(folderStructure[dir], folderStructure, result);
                }else if(val == string.Empty)
                {
                    //Console.WriteLine("fuck empty string" + result);
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
            string[] commands = input.Split("$");
            string[] filteredCommands = commands.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            
            Dictionary<string, string[]> folderStructure = new Dictionary<string, string[]>();
            Stack<String> directoryStack = new Stack<string>();
            string dir = string.Empty;
            //part1
            foreach(string command in filteredCommands)
            {
                
                
                if (command.Contains("cd")) dir = ReplaceFirst(command.Replace(" ", string.Empty).Replace("\n", string.Empty), "cd", string.Empty);
                else{ // ls
                    string[] clearCommand = ReplaceFirst(command, "ls\n", string.Empty).Split("\n")
                                                        .Select(x => x.StartsWith(" ") ? ReplaceFirst(x, " ", string.Empty) : x).ToArray();

                    //Console.WriteLine(command);
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

/*
            Dictionary<string, string> cwd = new Dictionary<string, string>();
            cwd.Add("/", " dir a \n 14848514 b.txt \n 8504156 c.dat \n dir d");
            cwd.Add("a", " dir e \n 29116 f \n 2557 g \n 62596 h.lst");
*/
            foreach(var key in folderStructure)
            {
                int result = 0;
                //Console.WriteLine(key.Key + "-----");
                //foreach(var we in key.Value) Console.WriteLine("|"+ we);
                //Console.WriteLine();
            }
            
            foreach(var key in folderStructure)
            {
                int result = 0;
                Console.WriteLine(key.Key + "-----" + getValue( key.Value, folderStructure, result));
                //Console.WriteLine();
            }


            //part2
            //Console.WriteLine(we.OrderBy(p => p).Reverse().Take(3).Sum());
        }
    }
}
