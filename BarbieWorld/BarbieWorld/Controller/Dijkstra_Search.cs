using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BarbieWorld.Models;
using Dijkstra.NET.Graph;
using Dijkstra.NET.ShortestPath;

namespace BarbieWorld.Controller
{
    public class Dijkstra_Search
    {
        // TODO: Reorganizar o path, ele ainda não recebe corretamente os valores que deveria!
        // TODO: Garantir que o loop de amigos contabiliza o caminho todo.
        public List<uint> search(Position StartPoint, List<MapCell> Cells, List<NPC> npcs)
        {
            var graph = new Graph<int, string>();
            foreach(MapCell cell in Cells)
            {
                foreach(int child in cell.Children)
                {
                    graph.AddNode(cell.id);
                }
            }

            foreach (MapCell cell in Cells)
            {
                foreach (int child in cell.Children)
                {
                    graph.Connect((uint)cell.id, (uint)child, cell.Weight, "");
                }
            }
            //Lista dos Resultados
            ShortestPathResult result;
            //Qual NPC mais perto em linha reta
            NPC neighbour = NextNeighbour(StartPoint, npcs);
            //Remove da Lista para não contabiliza-lo na proxima chamada
            npcs.Remove(neighbour);
            //Adciona o resultado do Dijkstra na lista dos resultados
            result = graph.Dijkstra((uint)(Cells.Where(c => c.Pos.Posx == StartPoint.Posx && c.Pos.Posy == StartPoint.Posy ).ToList().First().id), (uint)(Cells.Where(c => c.Pos.Posx == neighbour.Pos.Posx && c.Pos.Posy == neighbour.Pos.Posy).ToList().First().id));
            //Coloca a Barbie em sua nova posição
            StartPoint = neighbour.Pos;
            int loop = 0;
            //Contabiliza a resposta caso NPC tenha aceitado
            if (neighbour.Ansr) loop += 1;
            //Cria o caminho e acrescenta valores
            var path = result.GetPath();
            //Loop the rest
            do
            {
                neighbour = NextNeighbour(StartPoint, npcs);
                npcs.Remove(neighbour);
                result = graph.Dijkstra((uint)(Cells.Where(c => c.Pos.Posx == StartPoint.Posx && c.Pos.Posy == StartPoint.Posy).ToList().First().id), (uint)(Cells.Where(c => c.Pos.Posx == neighbour.Pos.Posx && c.Pos.Posy == neighbour.Pos.Posy).ToList().First().id));
                StartPoint = neighbour.Pos;
                if (neighbour.Ansr) loop += 1;
                path.Concat(result.GetPath());
            } while (loop < 3);

            return path.ToList();
        }

        //Function to choose first neighbour
        public NPC NextNeighbour(Position init, List<NPC> npcs)
        {
            uint shortest = 100000000;
            NPC closest = new NPC();
            foreach(NPC npc in npcs)
            {
                uint aux = (uint)Math.Sqrt(Math.Pow(Math.Abs(init.Posx - npc.Pos.Posx), 2) + Math.Pow(Math.Abs(init.Posy - npc.Pos.Posy), 2));
                if (aux < shortest)
                {
                    shortest = aux;
                    init = npc.Pos;
                    closest = npc;
                }
            }
            return closest;
        }
    }
}
