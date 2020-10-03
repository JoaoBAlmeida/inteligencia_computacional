using DeepSearch_Labyrinth.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DeepSearch_Labyrinth.Services
{
    public class SearchService
    {
		private static readonly string START = "S";
		private static readonly string GOAL = "G";
		private static readonly string WALL = "0";
		private Node root = new Node();
		private string[,] matriz;
		private int row = 0;
		private int col = 0;
		private int searchCost = 0;
		private int pathCost = 0;
		private LinkedList<string> path = new LinkedList<string>();
		private LinkedList<string> list = new LinkedList<string>();
        private LinkedList<Node> ramifications = new LinkedList<Node>();
		private Queue<Node> tree = new Queue<Node>();

		public SearchService(string nameMap)
        {
			MapReader mr = new MapReader();
			matriz = mr.getMap(nameMap);
			row = mr.getRows();
			col = mr.getColumns();
			root = getNodeRoot();
			list.AddLast(root.State);
			tree.Enqueue(root);
        }

		private Node getNodeRoot()
        {
			Node node = null;
			for (int l = 0; l < row; l++)
			{
				for (int c = 0; c < col; c++)
				{
					if (matriz[l,c].Equals(START))
					{
						node = new Node();
						node.Weight = 0;
						node.State = l + ":" + c;
						return node;
					}
				}
			}
			return node;
		}

        public void Widesolver()
        {
            string[] param = null;
            int lin = 0;
            int col = 0;
            while (!(tree.Count == 0))
            {
                Node node = tree.Dequeue();
                param = node.State.Split(":");
                lin = Convert.ToInt32(param[0]);
                col = Convert.ToInt32(param[1]);
                if (matriz[lin,col].Equals(GOAL))
                {
                    Node aux = node.Father;
                    path.AddLast(node.State);
                    while (aux != null)
                    {
                        pathCost++;
                        path.AddLast(aux.State);
                        aux = aux.Father;
                    }
                    tree.Clear();
                }
                else
                {
                    searchCost++;
                    //analisa norte
                    if ((lin - 1) >= 0 && (lin - 1) < row && !matriz[lin - 1,col].Equals(WALL))
                    {
                        if (!list.Contains((lin - 1) + ":" + col))
                        {
                            Node norte = new Node();
                            norte.Weight = 1;
                            norte.State = (lin - 1) + ":" + col;
                            norte.Father = node;
                            tree.Enqueue(norte);
                            list.AddLast(norte.State);
                        }
                    }

                    //analisa leste
                    if ((col + 1) < this.col && !matriz[lin,col + 1].Equals(WALL))
                    {
                        if (!list.Contains(lin + ":" + (col + 1)))
                        {
                            Node leste = new Node();
                            leste.Weight = 1;
                            leste.State = lin + ":" + (col + 1);
                            leste.Father = node;
                            tree.Enqueue(leste);
                            list.AddLast(leste.State);
                        }
                    }

                    //analisa oeste
                    if ((col - 1) >= 0 && (col - 1) < this.col && !matriz[lin,col - 1].Equals(WALL))
                    {
                        if (!list.Contains(lin + ":" + (col - 1)))
                        {
                            Node oeste = new Node();
                            oeste.Weight = 1;
                            oeste.State = lin + ":" + (col - 1);
                            oeste.Father = node;
                            tree.Enqueue(oeste);
                            list.AddLast(oeste.State);
                        }
                    }

                    //analisa sul
                    if ((lin + 1) < row && !matriz[lin + 1,col].Equals(WALL))
                    {
                        if (!list.Contains((lin + 1) + ":" + col))
                        {
                            Node sul = new Node();
                            sul.Weight = 1;
                            sul.State = (lin + 1) + ":" + col;
                            sul.Father = node;
                            tree.Enqueue(sul);
                            list.AddLast(sul.State);
                        }
                    }

                }

            }

        }

		public void Deepsolver()
        {
            string[] param = null;
            int lin = 0;
            int col = 0;
            while (!(tree.Count == 0))
            {
                Node node = tree.Dequeue();
                param = node.State.Split(":");
                lin = Convert.ToInt32(param[0]);
                col = Convert.ToInt32(param[1]);
                if (matriz[lin, col].Equals(GOAL))
                {
                    Node aux = node.Father;
                    path.AddLast(node.State);
                    while (aux != null)
                    {
                        pathCost++;
                        path.AddLast(aux.State);
                        aux = aux.Father;
                    }
                    tree.Clear();
                }
                else
                {
                    searchCost++;
                    //analisa norte
                    if ((lin - 1) >= 0 && (lin - 1) < row && !matriz[lin - 1, col].Equals(WALL) && !list.Contains((lin - 1) + ":" + col))
                    {
                        if ((col + 1) < this.col && !matriz[lin, col + 1].Equals(WALL) && !list.Contains(lin + ":" + (col + 1)) ||
                            (lin + 1) < row && !matriz[lin + 1, col].Equals(WALL) && !list.Contains((lin + 1) + ":" + col) ||
                            (col - 1) >= 0 && (col - 1) < this.col && !matriz[lin, col - 1].Equals(WALL) && !list.Contains(lin + ":" + (col - 1)))
                        {
                            Node ram = new Node();
                            if (matriz[lin, col].Equals(START)) ram.Weight = 0;
                            else ram.Weight = 1;
                            ram.State = lin + ":" + col;
                            ram.Father = node;
                            ramifications.AddLast(ram);
                        }
                        Node norte = new Node();
                        norte.Weight = 1;
                        norte.State = (lin - 1) + ":" + col;
                        norte.Father = node;
                        tree.Enqueue(norte);
                        list.AddLast(norte.State);
                    }

                    //analisa leste
                    else if ((col + 1) < this.col && !matriz[lin, col + 1].Equals(WALL) && !list.Contains(lin + ":" + (col + 1)))
                    {
                        if ((lin - 1) >= 0 && (lin - 1) < row && !matriz[lin - 1, col].Equals(WALL) && !list.Contains((lin - 1) + ":" + col) ||
                            (lin + 1) < row && !matriz[lin + 1, col].Equals(WALL) && !list.Contains((lin + 1) + ":" + col) ||
                            (col - 1) >= 0 && (col - 1) < this.col && !matriz[lin, col - 1].Equals(WALL) && !list.Contains(lin + ":" + (col - 1)))
                        {
                            Node ram = new Node();
                            if (matriz[lin, col].Equals(START)) ram.Weight = 0;
                            else ram.Weight = 1;
                            ram.State = lin + ":" + col;
                            ram.Father = node;
                            ramifications.AddLast(ram);
                        }
                        Node leste = new Node();
                        leste.Weight = 1;
                        leste.State = lin + ":" + (col + 1);
                        leste.Father = node;
                        tree.Enqueue(leste);
                        list.AddLast(leste.State);
                    }

                    //analisa sul
                    else if ((lin + 1) < row && !matriz[lin + 1, col].Equals(WALL) && !list.Contains((lin + 1) + ":" + col))
                    {
                        if ((lin - 1) >= 0 && (lin - 1) < row && !matriz[lin - 1, col].Equals(WALL) && !list.Contains((lin - 1) + ":" + col) ||
                            (col + 1) < this.col && !matriz[lin, col + 1].Equals(WALL) && !list.Contains(lin + ":" + (col + 1)) ||
                            (col - 1) >= 0 && (col - 1) < this.col && !matriz[lin, col - 1].Equals(WALL) && !list.Contains(lin + ":" + (col - 1)))
                        {
                            Node ram = new Node();
                            if (matriz[lin, col].Equals(START)) ram.Weight = 0;
                            else ram.Weight = 1;
                            ram.State = lin + ":" + col;
                            ram.Father = node;
                            ramifications.AddLast(ram);
                        }
                        Node sul = new Node();
                        sul.Weight = 1;
                        sul.State = (lin + 1) + ":" + col;
                        sul.Father = node;
                        tree.Enqueue(sul);
                        list.AddLast(sul.State);  
                    }

                    //analisa oeste
                    else if ((col - 1) >= 0 && (col - 1) < this.col && !matriz[lin, col - 1].Equals(WALL) && !list.Contains(lin + ":" + (col - 1)))
                    {
                        if ((lin - 1) >= 0 && (lin - 1) < row && !matriz[lin - 1, col].Equals(WALL) && !list.Contains((lin - 1) + ":" + col) ||
                            (col + 1) < this.col && !matriz[lin, col + 1].Equals(WALL) && !list.Contains(lin + ":" + (col + 1)) ||
                            (lin + 1) < row && !matriz[lin + 1, col].Equals(WALL) && !list.Contains((lin + 1) + ":" + col))
                        {
                            Node ram = new Node();
                            if (matriz[lin, col].Equals(START)) ram.Weight = 0;
                            else ram.Weight = 1;
                            ram.State = lin + ":" + col;
                            ram.Father = node;
                            ramifications.AddLast(ram);
                        }
                        Node oeste = new Node();
                        oeste.Weight = 1;
                        oeste.State = lin + ":" + (col - 1);
                        oeste.Father = node;
                        tree.Enqueue(oeste);
                        list.AddLast(oeste.State);
                    }

                    //Retorna para uma Ramificação Anterior
                    else
                    {
                        Node node1 = ramifications.Last.Value;
                        ramifications.RemoveLast();
                        tree.Enqueue(node1);
                    }
                }

            }
        }

        public void printMap()
        {
            Console.WriteLine("Map");
            Console.Write(" ");
            for (int c = 0; c < col; c++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            for (int l = 0; l < row; l++)
            {
                Console.Write("|");
                for (int c = 0; c < col; c++)
                {
                    if (matriz[l, c] == "1") Console.Write(" ");
                    else if (matriz[l, c] == "0") Console.Write("X");
                    else if (matriz[l, c] == "G") Console.Write("G");
                    else if (matriz[l, c] == "S") Console.Write("S");
                }
                Console.Write("|");
                if(l == row - 1)
                {
                    Console.WriteLine();
                    Console.Write(" ");
                    for (int c = 0; c < col; c++)
                    {
                        Console.Write("-");
                    }
                }
                Console.WriteLine();
            }
            
            Console.WriteLine();
        }

        public void printSearch()
		{
			Console.WriteLine("Search");
			for (int l = 0; l < row; l++)
			{
				Console.Write("|");
				for (int c = 0; c < col; c++)
				{
					if (list.Contains(l + ":" + c))
					{
						Console.Write("X|");
					}
					else
					{
						Console.Write(" |");
					}
				}
				Console.WriteLine();
			}
			Console.WriteLine("Order ");
            foreach(string place in list)
            {
                Console.Write(place + " - ");
            }
			Console.WriteLine();
		}

        public void printMapPath()
        {
            Console.WriteLine("Map Path");
            Console.Write(" ");
            for (int c = 0; c < col; c++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
            for (int l = 0; l < row; l++)
            {
                Console.Write("|");
                for (int c = 0; c < col; c++)
                {
                    if (matriz[l, c] == "1")
                    {
                        if (path.Contains(l + ":" + c)) Console.Write((char)33);
                        else Console.Write(" ");
                    }
                    else if (matriz[l, c] == "0") Console.Write("X");
                    else if (matriz[l, c] == "G") Console.Write("G");
                    else if (matriz[l, c] == "S") Console.Write("S");
                }
                Console.Write("|");
                if (l == row - 1)
                {
                    Console.WriteLine();
                    Console.Write(" ");
                    for (int c = 0; c < col; c++)
                    {
                        Console.Write("-");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Order");
            foreach (string way in path)
            {
                Console.Write(way + " - ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }


        public int getTotalCost()
		{
			return searchCost + pathCost;
		}

		public int getSearchCost()
		{
			return searchCost;
		}

		public int getPathCost()
		{
			return pathCost;
		}
	}
}
