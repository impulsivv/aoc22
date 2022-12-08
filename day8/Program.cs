using System;
using System.Linq;

namespace day1
{
    class Program
    {
        static int[] GetColumn(int[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                    .Select(x => matrix[x, columnNumber])
                    .ToArray();
        }

        static int[] GetRow(int[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j] + ",");
                }
                Console.WriteLine();
            }
        }

        static int CheckVisible(int row, int col, int[,] matrix)
        {
            int[] rowArray = GetRow(matrix, row);
            int[] colArray = GetColumn(matrix, col);
            if(rowArray[col] > rowArray[.. col].Max() || rowArray[col] > rowArray[(col+1) ..].Max()) return 1;
            else if(colArray[row] > colArray[.. row].Max() || colArray[row] > colArray[(row+1) ..].Max()) return 1;
            else return 0;
        }

        static int ScenicScore(int row, int col, int[,] matrix)
        {
            int[] rowArray = GetRow(matrix, row);
            int[] colArray = GetColumn(matrix, col);
            int val = rowArray[col];
            
            int leftView = rowArray[.. col].Reverse().ToList().FindIndex(x => x >= val);
            leftView = leftView == -1 ? rowArray[.. col].Length : leftView + 1;

            int rightView = rowArray[(col+1) ..].ToList().FindIndex(x => x >= val);
            rightView = rightView == -1 ? rowArray[(col+1) ..].Length : rightView + 1;
 
            int topView = colArray[.. row].Reverse().ToList().FindIndex(x => x >= val);
            topView = topView == -1 ? colArray[.. row].Length : topView + 1;

            int botView = colArray[(row+1) .. ].ToList().FindIndex(x => x >= val);
            botView = botView == -1 ? colArray[(row+1) .. ].Length : botView + 1;

            return leftView * rightView * topView * botView; 
        }

        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"./input.txt").Replace("\r", string.Empty);
            string[] rows = input.Split("\n");
            int rowLen = rows[0].Length;
            int columnLen = rows.Length;
            int[,] matrix = new int[rowLen, columnLen];
            for(int row=0; row < rowLen; row++)
            {
                for(int col=0; col < columnLen; col++)
                {
                    matrix[row,col] = Int32.Parse(rows[row][col].ToString());
                    //Console.WriteLine(Int32.Parse(rows[row][col]));
                }
            }

            //part1
            int result = 0;
            for(int row=1; row < rowLen - 1; row++)
            {
                for(int col=1; col < columnLen - 1; col++)
                {
                    result += CheckVisible(row, col, matrix);
                    //Console.WriteLine("row" + row + " col" + col + " -> " + CheckVisible(row, col, matrix));
                }
            }
            //add edge trees
            result += 2 * rowLen + 2 * columnLen - 4;
            Console.WriteLine(result);

            
            //foreach(int i in test[1 ..]) Console.WriteLine(i);

            //Console.WriteLine(rows[0].Length);
            //part2
            Console.WriteLine(ScenicScore(1, 2, matrix));

            List<int> results = new List<int>();
            for(int row=0; row < rowLen ; row++)
            {
                for(int col=0; col < columnLen ; col++)
                {
                    results.Add(ScenicScore(row, col, matrix));
                    //Console.WriteLine("row" + row + " col" + col + " -> " + CheckVisible(row, col, matrix));
                }
            }
            Console.WriteLine(results.Max());

        }
    }
}
