using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AStarNavigator;
using AStarNavigator.Algorithms;
using AStarNavigator.Providers;
using BarbieWorld.Models;

namespace BarbieWorld.Controller
{
    public class AStar_Search
    {
        public List<uint> search(Position StartPoint, List<MapCell> Cells, List<NPC> npcs)
        {
            //Variavél do caminho
            IEnumerable<Tile>[] path = new IEnumerable<Tile>[npcs.Count + 1];

            var navigator = new TileNavigator(
                new EmptyBlockedProvider(),         // Instance of: IBockedProvider
                new DiagonalNeighborProvider(),     // Instance of: INeighborProvider
                new PythagorasAlgorithm(),          // Instance of: IDistanceAlgorithm
                new ManhattanHeuristicAlgorithm()   // Instance of: IDistanceAlgorithm
            );

            int loop = 0, aux = 0;
            do
            {
                //Qual NPC mais perto em linha reta
                NPC neighbour = NextNeighbour(StartPoint, npcs);
                //Remove da Lista para não contabiliza-lo na proxima chamada
                npcs.Remove(neighbour);

                var from = new Tile(StartPoint.Posx, StartPoint.Posy);
                var to = new Tile(neighbour.Pos.Posx, neighbour.Pos.Posy);

                path[aux] = navigator.Navigate(from, to);

                //Coloca a Barbie em sua nova posição
                StartPoint = neighbour.Pos;
                //Contabiliza a resposta caso NPC tenha aceitado
                if (neighbour.Ansr) loop += 1;

                //Mantém o auxiliar crescendo
                aux++;
            } while (loop < 3);

            //Convert path para algo que possa ser lido no front
            List<uint> response = new List<uint>();
            foreach(IEnumerable<Tile> tile in path)
            {
                if(!(tile == null))
                    foreach(Tile position in tile)
                    {
                        response.Add((uint)(Cells.Where(c => c.Pos.Posx == position.X && c.Pos.Posy == position.Y).ToList().First().id));
                    }
            }
            return response;
        }
        public NPC NextNeighbour(Position init, List<NPC> npcs)
        {
            uint shortest = 100000000;
            NPC closest = new NPC();
            foreach (NPC npc in npcs)
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
