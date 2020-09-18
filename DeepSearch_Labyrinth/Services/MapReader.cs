using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeepSearch_Labyrinth.Services
{
    public class MapReader
    {
        private int row;
        private int col;

        public string[,] getMap (string name)
        {
            List<string> lines = new List<string>();
            int counter = 0;
            try
            {
                using (StreamReader file = new StreamReader("C:/Users/jpbar/source/repos/DeepSearch_Labyrinth/Maps/" + name + ".txt"))
                {
                    string ln;
                    while((ln = file.ReadLine()) != null)
                    {
                        lines.Add(ln);
                        counter++;
                    }
                    col = lines[0].Split(",").Length;
                    file.Close();
                }
            }catch(Exception e)
            {
                Console.WriteLine("Somethin went wrong: " + e.Message);
            }

            return processMap(lines, counter);
        }

        private string[,] processMap(List<string> lines, int rows)
        {
            row = rows;
            string[,] mat = new string[rows,col];
            int i = 0;
            foreach(string line in lines)
            {
                string[] vet = line.Split(",");
                for (int c = 0; c < vet.Length; c++) mat[i,c] = vet[c].Trim();
                i++;
            }
            return mat;
        }

        public int getRows()
        {
            return row;
        }

        public int getColumns()
        {
            return col;
        }
    }
}
