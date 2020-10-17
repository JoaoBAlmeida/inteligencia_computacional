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
        API_Connect BW = new API_Connect();

        [HttpGet]
        public Object[] GetBW_Dijkstra()
        {
            Object[] response = new Object[4]{
                BW.GetMap(), BW.GetNPCs(), BW.GetPath_Dijkstra(), BW.GetCost_Dijkstra()
            };
            return response;
        }

        [HttpGet("GetAStar")]
        public Object[] GetBW()
        {
            Object[] response = new Object[4]{
                BW.GetMap(), BW.GetNPCs(), BW.GetPath(), BW.GetCost()
            };
            return response;
        }

        [HttpGet("GetPathDij")]
        public List<IEnumerable<uint>> GetPath_Dij()
        {
            return BW.GetPath_Dijkstra();
        }

        [HttpGet("GetPath")]
        public List<uint> GetPath()
        {
            return BW.GetPath();
        }

        [HttpGet("GetNPC")]
        public List<NPC> SeeNPC()
        {
            return BW.GetNPCs();
        }

        [HttpGet("GetMAP")]
        public List<MapCell> SeeMap()
        {
            return BW.GetMap();
        }

        [HttpGet("GetCost")]
        public List<uint> WalkCost()
        {
            return BW.GetCost();
        }
    }
}
