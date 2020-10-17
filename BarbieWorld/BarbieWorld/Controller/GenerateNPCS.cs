﻿using BarbieWorld.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace BarbieWorld.Controller
{
    public class GenerateNPCS
    {
        private List<NPC> CreateNPCS(List<MapCell> cells, int qtd)
        {
            List<NPC> npcs = new List<NPC>();
            for (int x = 0; x < qtd; x++)
            {
                Position position = new Position();
                do
                {
                    position = new Position
                    {
                        Posx = new Random().Next(41)+1,
                        Posy = new Random().Next(41)+1
                    };
                } 
                while (!(cells.Where(c => c.Pos.Posx.Equals(position.Posx) && c.Pos.Posy.Equals(position.Posy)).ToList().First().Habitable));

                npcs.Add(new NPC
                {
                    Pos = position,
                    Ansr = false
                });
            }
            return npcs;
        }

        public List<NPC> SetNPCs(List<MapCell> cells, int qtd = 6)
        {
            List<NPC> npcs = CreateNPCS(cells, qtd);
            int count = 0;
            while(count < 3)
            {
                count = 0;
                npcs[new Random().Next(qtd)].Ansr = true;
                foreach (NPC npc in npcs)
                {
                    if (npc.Ansr) count++;
                }
            }
            
            return npcs;
        }
    }
}
