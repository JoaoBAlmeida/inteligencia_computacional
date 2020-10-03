using BarbieWorld.Controller;
using BarbieWorld.Models;
using BarbieWorld.Template;
using System;
using System.Collections.Generic;

namespace BarbieWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Barbie World");
            Console.WriteLine("=====================");
            WorldReader world = new WorldReader("BarbieWorld_Origin");
            List<MapCell> cells = world.ProcessMap();
            /* Check Map Cells creation */
            foreach(MapCell cell in cells)
            {
                Console.WriteLine(cell.ToString());
            }
            GenerateNPCS generator = new GenerateNPCS();
            List<NPC> npcs = generator.SetNPCs(cells);
            foreach(NPC npc in npcs)
            {
                Console.WriteLine(npc.ToString());
            }
        }
    }
}
