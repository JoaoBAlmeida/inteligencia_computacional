using System;
using System.Collections.Generic;
using System.Text;
using BarbieWorld.Models;
using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

namespace BarbieWorld.Controller
{
    public class Dijkstra_Search
    {
        public IEnumerable<uint> search(List<MapCell> Cells, List<NPC> npcs)
        {
            var graph = new Graph<int, string>();
            foreach(MapCell cell in Cells)
            {
                graph.AddNode(cell.id);
                foreach(int child in cell.Children)
                {
                    graph.Connect((uint)cell.id, (uint)child, cell.Weight, "");
                }
            }

            //Function to choose first neighbour

            ShortestPathResult result = graph.Dijkstra(30*25, (uint)(npcs[0].Pos.Posx*npcs[0].Pos.Posy)); //result contains the shortest path

            IEnumerable<uint> path = result.GetPath();
            return path;
        }
    }
}
