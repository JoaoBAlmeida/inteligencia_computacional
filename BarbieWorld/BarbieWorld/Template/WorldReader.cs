using BarbieWorld.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;

namespace BarbieWorld.Template
{
    public class WorldReader : MapReader
    {
        public WorldReader(string name)
        {
            GetMap(name);
        }

        public override List<MapCell> ProcessCell()
        {
            List<MapCell> Cell = new List<MapCell>();
            int i = 0;
            foreach(string line in lines)
            {
                string[] vet = line.Split(",");
                for(int c = 0; c < vet.Length; c++)
                {
                    Cell.Add(new MapCell
                    {
                        Weight = Convert.ToInt32(vet[c].Trim()),
                        Color = SetColor(Convert.ToInt32(vet[c].Trim())),
                        Habitable = Convert.ToInt32(vet[c].Trim()) == 0?false:true,
                        Pos = new Position { Posx = i, Posy = c}
                    });
                }
                i++;
            }
            return Cell;
        }

        public override string[,] ProcessMap()
        {
            string[,] mat = new string[GetRows(), GetCols()];
            int i = 0;
            foreach (string line in lines)
            {
                string[] vet = line.Split(",");
                for (int c = 0; c < vet.Length; c++) mat[i, c] = vet[c].Trim();
                i++;
            }
            return mat;
        }

        public Color SetColor(int reference)
        {
            switch (reference)
            {
                case 0:
                    return Color.Orange;
                case 1:
                    return Color.Gray;
                case 3:
                    return Color.Brown;
                case 5:
                    return Color.Green;
                case 10:
                    return Color.White;
                default:
                    return Color.Transparent;
            }
        }
    }
}
