using System;
using System.Collections.Generic;
using System.Text;
using BarbieWorld.Controller;
using BarbieWorld.Models;
using BarbieWorld.Template;

namespace BarbieWorld.Strategy
{
    public abstract class SearchService
    {
        protected Node root = new Node();
        protected List<NPC> npcs = new List<NPC>();
        protected string[,] matriz;
        protected int row = 0;
        protected int col = 0;
        protected int searchCost = 0;
        protected int pathCost = 0;
        protected LinkedList<string> path = new LinkedList<string>();
        protected LinkedList<Position> list = new LinkedList<Position>();
        protected Queue<Node> tree = new Queue<Node>();

        public void SetStage(string fileName)
        {
            WorldReader wr = new WorldReader(fileName);
            matriz = wr.ProcessMap();
            GenerateNPCS generator = new GenerateNPCS();
            npcs = generator.SetNPCs(wr.ProcessCell());
            row = wr.GetRows();
            col = wr.GetCols();
            root = NodeRoot();
            list.AddLast(root.Pos);
            tree.Enqueue(root);
        }

        public Node NodeRoot()
        {
            return new Node
            {
                Pos = new Position { Posx = 19, Posy = 23 }
            };
        }
    }
}
