using BarbieWorld.Models;
using BarbieWorld.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarbieWorld.Controller
{
    public class API_Connect
    {
        private List<MapCell> Cells;
        protected List<NPC> Npcs;
        public List<NPC> NpcsAux = new List<NPC>();
        private List<uint> Path;

        public API_Connect()
        {
            //Leitura do Mapa
            WorldReader wr = new WorldReader("BarbieWorld_Origin");
            Cells = wr.ProcessCell();
            //Gerando NPCS
            GenerateNPCS generator = new GenerateNPCS();
            Npcs = generator.SetNPCs(Cells);
            //Por motivo de passagem da referência, atribuição direta causa ambos a serem modificados.
            //Solução: Construção externa da lista auxiliar a ser modificada
            Npcs.ForEach((el) =>
            {
                NpcsAux.Add(el);
            });
            //Reconhecendo o Caminho Andando
            Dijkstra_Search DJ_Search = new Dijkstra_Search();
            Path = DJ_Search.search(new Position { Posx = 1, Posy = 1}, Cells, NpcsAux);
        }

        public List<MapCell> GetMap()
        {
            return Cells;
        }

        public List<NPC> GetNPCs()
        {
            return Npcs;
        }

        public List<uint> GetPath()
        {
            return Path;
        }
    }

    static class Extensions
    {
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
