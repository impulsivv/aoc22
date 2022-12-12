using System;
using System.Linq;

namespace day1
{
    class Program
    {
        static bool checkCycle(int curCycle)
        {
            if ( ((curCycle + 20) % 40) == 0 ) return true;
            else return false;
        }
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] cmds = input.Split("\n").Select(x => x.Replace("addx", "2").Replace("noop", "1 0").ToString()).ToArray();
            int curCycle = 0;
            int register = 1;
            List<int> result = new List<int>();

           (int c, int x)[] numCmds = cmds.Select(cmd => 
            {
                string[] cmdAr = cmd.Split(" ");
                int cycleCost = Int32.Parse(cmdAr[0]);
                int x = Int32.Parse(cmdAr[1]);
                return (cycleCost, x);
            }).ToArray();

            foreach((int c, int x) cmd in numCmds)
            {
                if(cmd.c == 1)
                {
                    curCycle += 1;
                    if( checkCycle(curCycle) )
                    {
                        //Console.WriteLine(curCycle + " " + cmd.x);
                        result.Add(curCycle * register );
                    } 
                } 
                else
                {
                   for(int i=0; i < 2; i++)
                    {
                        curCycle += 1;
                        if( checkCycle(curCycle))
                        {
                            //Console.WriteLine(curCycle + " " + cmd.x);
                            result.Add(curCycle * register );
                        } 
                    }
                    register += cmd.x;
                }
            }
            //part1
            Console.WriteLine(result.Sum());
   
            string[,] crt = new string[6,40];
            int rowPos = 0;
            curCycle = 0;
            register = 1;

            /*crt[0,0] = "F";
            crt[1,0] = "a";
            crt[0,1] = "g";          

            foreach(var row in crt)
            {
                //Console.WriteLine(row);
            }
            //Console.WriteLine(crt[0,0]);*/
            //part2
            
            foreach((int c, int x) cmd in numCmds)
            {
                if(cmd.c == 1)
                {
                    if (Enumerable.Range(register,3).Contains(curCycle + 1)) crt[rowPos, curCycle] = "#";
                    else crt[rowPos, curCycle] = " ";
                    //crt[rowPos, curCycle] = string.Empty;
                    //crt[rowPos, curCycle] = "#";
                    curCycle += 1;
                    if( (curCycle % 40) == 0 )
                    {
                        curCycle = 0;
                        rowPos += 1;
                        //Console.WriteLine(curCycle + " " + cmd.x);
                        result.Add(curCycle * register );
                    } 
                } 
                else
                {
                   for(int i=0; i < 2; i++)
                    {
                        if (Enumerable.Range(register,3).Contains(curCycle + 1)) crt[rowPos, curCycle] = "#";
                        else crt[rowPos, curCycle] = " ";
                        curCycle += 1;
                        if( (curCycle % 40) == 0 )
                        {
                            curCycle = 0;
                            rowPos += 1;
                            //Console.WriteLine(curCycle + " " + cmd.x);
                            result.Add(curCycle * register );
                        } 
                    }
                    register += cmd.x;
                }
                //Console.WriteLine(curCycle + " " + register);
                //Console.WriteLine(cmd);
                

            }
            for(int i=0; i < 6 ; i++)
            {
                for(int j=0; j < 40 ; j++)
                {
                    Console.Write(crt[i, j]);
                }
                Console.WriteLine("");
            }

        }
    }
}
