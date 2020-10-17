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
        API_Connect BW = new API_Connect();

        [HttpGet]
        public Object[] GetBW()
        {
            Object[] response = new Object[3]{
                BW.GetMap(), BW.GetNPCs(), BW.GetPath()
            };
            return response;
        }

        [HttpGet("GetPath")]
        public List<IEnumerable<uint>> GetPath()
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
