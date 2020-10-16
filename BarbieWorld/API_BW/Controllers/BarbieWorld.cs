using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarbieWorld.Controller;
using BarbieWorld.Models;
using BarbieWorld.Strategy;
using BarbieWorld.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_BW.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarbieWorld : ControllerBase
    {
        
        //[HttpGet("GetCells")]
        public List<MapCell> GetCells()
        {
            WorldReader world = new WorldReader("BarbieWorld_Origin");
            List<MapCell> cells = world.ProcessCell();
            return cells;
        }

        //[HttpGet("GetNPCs")]
        public List<NPC> GetNPCs()
        {
            WorldReader world = new WorldReader("BarbieWorld_Origin");
            List<MapCell> cells = world.ProcessCell();
            GenerateNPCS generator = new GenerateNPCS();
            List<NPC> npcs = generator.SetNPCs(cells);
            return npcs;
        }
        
        [HttpGet("GetPath")]
        public IEnumerable<uint> GetPath()
        {
            WorldReader world = new WorldReader("BarbieWorld_Origin");
            List<MapCell> cells = world.ProcessCell();
            GenerateNPCS generator = new GenerateNPCS();
            List<NPC> npcs = generator.SetNPCs(cells);
            Dijkstra_Search DJ_Search = new Dijkstra_Search();
            IEnumerable<uint> Path = DJ_Search.search(cells, npcs);
            return Path;
        }
    }
}
