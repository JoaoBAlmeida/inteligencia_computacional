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
        protected List<MapCell> cells = new List<MapCell>();
        protected List<NPC> npcs = new List<NPC>();
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
            cells = wr.ProcessMap();
            GenerateNPCS generator = new GenerateNPCS();
            npcs = generator.SetNPCs(cells);
            row = wr.GetRows();
            col = wr.GetCols();
            root = NodeRoot();
            list.AddLast(root.Pos);
            tree.Enqueue(root);
        }

        public abstract Node NodeRoot();
    }
}
