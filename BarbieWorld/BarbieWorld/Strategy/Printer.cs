using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BarbieWorld.Strategy
{
    public class Printer
    {
        public void PrintMap(string[,] matriz, int row, int col)
        {
            Console.WriteLine("====MAP====");
            for(int l = 0; l < row; l++)
            {
                Console.Write('|');
                for(int c = 0; c < col; c++)
                {
                    Console.Write(matriz[l, c]);
                }
                Console.Write("|");
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
