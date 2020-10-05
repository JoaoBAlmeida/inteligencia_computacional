using BarbieWorld.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace BarbieWorld.Template
{
    public abstract class MapReader
    {
        private int row;
        private int col;
        protected List<string> lines;

        protected void GetMap (string name)
        {
            lines = new List<string>();
            int counter = 0;
            try
            {
                using (StreamReader file = new StreamReader("C:/Users/jpbar/source/repos/BarbieWorld/Maps/" + name + ".txt"))
                {
                    string ln;
                    while((ln = file.ReadLine()) != null)
                    {
                        lines.Add(ln);
                        counter++;
                    }
                    row = counter;
                    col = lines[0].Split(",").Length;
                    file.Close();
                }
            }catch(Exception e)
            {
                Console.WriteLine("Read File ERROR: " + e.Message);
            }
        }

        public int GetRows()
        {
            return this.row;
        }

        public int GetCols()
        {
            return this.col;
        }

        public abstract List<MapCell> ProcessCell();
        public abstract string[,] ProcessMap();
    }
}
