using BarbieWorld.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
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
            int id = 0;
            int i = 0;
            foreach(string line in lines)
            {
                string[] vet = line.Split(",");
                for(int c = 0; c < vet.Length; c++)
                {
                    Cell.Add(new MapCell
                    {
                        id = id,
                        Weight = Convert.ToInt32(vet[c].Trim()),
                        Color = SetColor(Convert.ToInt32(vet[c].Trim())),
                        Habitable = Convert.ToInt32(vet[c].Trim()) == 0 ? false : true,
                        Pos = new Position { Posx = i, Posy = c },
                        Children = new List<int>()
                    });
                    id++;
                }
                i++;
            }
            Cell = ChildrenAdd(Cell);
            return Cell;
        }

        public List<MapCell> ChildrenAdd(List<MapCell> Cells)
        {
            for(var x = 0; x < (col * row); x++)
            {
                //Esquerda
                if(x-1 >= 0)
                    Cells[x].Children.Add(x-1);
                //Direita
                if((x+1)%col != 0)
                    Cells[x].Children.Add(x+1);
                //Baixo
                if(x + col < row * col)
                    Cells[x].Children.Add(x + col);
                //Cima
                if(x - col >= 0)
                    Cells[x].Children.Add(x - col);
            }
            return Cells;
        }
        #region arvore por objeto
        /*
        public List<MapCell> mountTree(List<MapCell> Cells)
        {
            col -= 1;
            row -= 1;
            MapCell aux = new MapCell();
             foreach (MapCell cell in Cells)
             {
                 
                 //Para aqueles com dificuldades de entender, 
                 //acredite que e gritei socorro mais que você!

                 //Para posteridade:
                 //Col -1 == esquerda
                 //Row -1 == em cima
                 //Col +1 == direita
                 //Row +1 == embaixo

                 cell.Children = new List<MapCell>();
                 //FIRST POINT
                 if (cell.Pos.Posx == 0 && cell.Pos.Posy == 0)
                 {
                    //Aux Map Cell para retirar o loop de filhos
                    aux = Cells.Where(c => (c.Pos.Posx == 1 && c.Pos.Posy == 0)).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //=====
                    aux = Cells.Where(c => (c.Pos.Posx == 0 && c.Pos.Posy == 1)).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                 }
                 //FIRST ROW LAST POINT
                 else if (cell.Pos.Posx == col && cell.Pos.Posy == 0)
                 {
                    //Aux Map Cell para retirar o loop de filhos
                    aux = Cells.Where(c => c.Pos.Posx == col - 1 && c.Pos.Posy == 0).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //=====
                    aux = Cells.Where(c => c.Pos.Posx == col && c.Pos.Posy == 1).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                 }
                 //FIRST COLUMN LAST POINT
                 else if (cell.Pos.Posx == 0 && cell.Pos.Posy == row)
                 {
                    //Aux Map Cell para retirar o loop de filhos
                    aux = Cells.Where(c => c.Pos.Posx == 0 && c.Pos.Posy == row - 1).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //=======
                    aux = Cells.Where(c => c.Pos.Posx == 1 && c.Pos.Posy == row).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                 }
                 //LAST POINT
                 else if (cell.Pos.Posx == col && cell.Pos.Posy == row)
                 {
                    //Aux Map Cell para retirar o loop de filhos
                    aux = Cells.Where(c => c.Pos.Posx == col - 1 && c.Pos.Posy == row).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //=====
                    aux = Cells.Where(c => c.Pos.Posx == col && c.Pos.Posy == row - 1).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                 }
                 //FIRST COLUMN
                 else if (cell.Pos.Posx == 0 && !(cell.Pos.Posy == row) && cell.Pos.Posy > 0)
                 {
                    //Aux Map Cell para retirar o loop de filhos
                    aux = Cells.Where(c => c.Pos.Posy == cell.Pos.Posy - 1 && c.Pos.Posx == cell.Pos.Posx).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //=======
                    aux = Cells.Where(c => c.Pos.Posx == cell.Pos.Posx + 1 && c.Pos.Posy == cell.Pos.Posy).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //=======
                    aux = Cells.Where(c => c.Pos.Posy == cell.Pos.Posy + 1 && c.Pos.Posx == cell.Pos.Posx).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                 }
                 //FIRST ROW
                 else if (cell.Pos.Posy == 0 && !(cell.Pos.Posx == col) && cell.Pos.Posx > 0)
                 {
                    //Aux Map Cell para retirar o loop de filhos
                    aux = Cells.Where(c => c.Pos.Posx == cell.Pos.Posx - 1 && c.Pos.Posy == cell.Pos.Posy).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //=======
                    aux = Cells.Where(c => c.Pos.Posx == cell.Pos.Posx + 1 && c.Pos.Posy == cell.Pos.Posy).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //=======
                    aux = Cells.Where(c => c.Pos.Posy == cell.Pos.Posy + 1 && c.Pos.Posx == cell.Pos.Posx).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                 }
                 //LAST COLUMN
                 else if (cell.Pos.Posx == col && !(cell.Pos.Posy == row || cell.Pos.Posy == 0))
                 {
                    //Aux Map Cell para retirar o loop de filhos4
                    aux = Cells.Where(c => c.Pos.Posy == cell.Pos.Posy - 1 && c.Pos.Posx == cell.Pos.Posx).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //=======
                    aux = Cells.Where(c => c.Pos.Posy == cell.Pos.Posy + 1 && c.Pos.Posx == cell.Pos.Posx).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //========
                    aux = Cells.Where(c => c.Pos.Posx == cell.Pos.Posx - 1 && c.Pos.Posy == cell.Pos.Posy).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                 }
                 //LAST ROW
                 else if (cell.Pos.Posy == row && !(cell.Pos.Posx == col || cell.Pos.Posx == 0))
                 {
                    //Aux Map Cell para retirar o loop de filhos4
                    aux = Cells.Where(c => c.Pos.Posx == cell.Pos.Posx - 1 && c.Pos.Posy == cell.Pos.Posy).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //=======
                    aux = Cells.Where(c => c.Pos.Posx == cell.Pos.Posx + 1 && c.Pos.Posy == cell.Pos.Posy).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //========
                    aux = Cells.Where(c => c.Pos.Posy == cell.Pos.Posy - 1 && c.Pos.Posx == cell.Pos.Posx).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                 }
                 else
                 {
                    //Aux Map Cell para retirar o loop de filhos4
                    aux = Cells.Where(c => cell.Pos.Posx == c.Pos.Posx - 1 && c.Pos.Posy == cell.Pos.Posy).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //=======
                    aux = Cells.Where(c => cell.Pos.Posx == c.Pos.Posx + 1 && c.Pos.Posy == cell.Pos.Posy).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //========
                    aux = Cells.Where(c => cell.Pos.Posy == c.Pos.Posy - 1 && c.Pos.Posx == cell.Pos.Posx).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                    //========
                    aux = Cells.Where(c => cell.Pos.Posy == c.Pos.Posy + 1 && c.Pos.Posx == cell.Pos.Posx).ToList().First();
                    aux.Children = null;
                    cell.Children.Add(aux);
                 }
             }
            return Cells;
        }
        */
        #endregion
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
