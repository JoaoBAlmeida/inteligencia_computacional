using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BarbieWorld.Controller;
using BarbieWorld.Models;
using BarbieWorld.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_BW.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BarbieWorld : ControllerBase
    {

        [HttpGet("GetCells")]
        public List<MapCell> GetCells()
        {
            WorldReader world = new WorldReader("BarbieWorld_Origin");
            List<MapCell> cells = world.ProcessCell();
            return cells;
        }

        [HttpGet("GetNPCs")]
        public List<NPC> GetNPCs()
        {
            WorldReader world = new WorldReader("BarbieWorld_Origin");
            List<MapCell> cells = world.ProcessCell();
            GenerateNPCS generator = new GenerateNPCS();
            List<NPC> npcs = generator.SetNPCs(cells);
            return npcs;
        }
    }
}
