using System;
using System.Linq;

namespace day1
{
    class Program
    {
        static (int x, int y) doMove((int x, int y) curPos, string dir)
        {
            switch(dir)
            {
                case "U":
                    return (curPos.x ,curPos.y + 1);
                    
                case "D": 
                    return (curPos.x ,curPos.y - 1);
                    
                case "R": 
                    return (curPos.x + 1,curPos.y);
                    
                case "L": 
                    return (curPos.x - 1,curPos.y);
                    
                default: 
                    return curPos;
                    
            }                
        }

        static bool checkIfInRange((int x, int y) curPos, (int x, int y) prevPos)
        {
            if ((curPos.x + 1 == prevPos.x || curPos.x - 1 == prevPos.x || curPos.x  == prevPos.x) && (curPos.y + 1 == prevPos.y || curPos.y - 1 == prevPos.y || curPos.y  == prevPos.y )) return true;
            else return false;
        }
        static void doCmd(ref HashSet<(int, int)> tailPosMemory, ref (int, int) headPos, ref (int, int) tailPos, string cmd)
        {
            string[] cmdAr = cmd.Split(" ");
            string dir = cmdAr[0];
            int count = Int32.Parse(cmdAr[1]);
            (int x, int y) bufPos;
            //(int x, int y) prevPos = curPos;
            for(int i=0; i < count; i++)
            {
                bufPos = headPos;
                headPos = doMove(headPos, dir);
                if (checkIfInRange(tailPos, headPos))
                {
                    tailPosMemory.Add(tailPos);
                    
                }else{
                    tailPosMemory.Add(bufPos);
                    tailPos = bufPos;
                      
                }
            }
        }
        static void doCmd2(ref HashSet<(int, int)> tailPosMemory, ref List<(int, int)> snake, string cmd)
        {
            string[] cmdAr = cmd.Split(" ");
            string dir = cmdAr[0];
            int count = Int32.Parse(cmdAr[1]);
            int xdif = 0;
            int ydif = 0;
            for(int i=0; i < count; i++)
            {
                snake[0] = doMove(snake[0], dir);
                for(int j=0; j < 9 ; j++)
                {
                    xdif = (snake[j].Item1 - snake[j + 1].Item1) > 0 ? 1 : -1;
                    ydif = (snake[j].Item2 - snake[j + 1].Item2) > 0 ? 1 : -1;
                    if ((snake[j].Item1 - snake[j + 1].Item1) == 0) xdif = 0;
                    if ((snake[j].Item2 - snake[j + 1].Item2) == 0) ydif = 0;
                    
                    if (!checkIfInRange(snake[j + 1], snake[j]))//
                    {
                        snake[j + 1] = ((snake[j + 1].Item1 + xdif) , (snake[j + 1].Item2 + ydif));
                    }  
                }
                tailPosMemory.Add(snake[9]);
            }
        }
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] cmds = input.Split("\n");
            HashSet<(int, int)> tailPosMemory = new HashSet<(int, int)>();
            (int x, int y) headPos = (0, 0);
            (int x, int y) tailPos = (0, 0);


            //part1
            foreach(string cmd in cmds)
            {
                doCmd(ref tailPosMemory, ref headPos, ref tailPos, cmd);
                
            }         
            Console.WriteLine(tailPosMemory.Count);
                
   
            //part2
            HashSet<(int, int)> tailPosMemory2 = new HashSet<(int, int)>();
            List<(int x, int y)> snake = new List<(int x, int y)>();
            for(int i=0; i < 10; i++) snake.Add((0,0));
            
            
            foreach(string cmd in cmds)
            {
                doCmd2(ref tailPosMemory2, ref snake, cmd);
            }
            Console.WriteLine(tailPosMemory2.Count);

        }
    }
}
