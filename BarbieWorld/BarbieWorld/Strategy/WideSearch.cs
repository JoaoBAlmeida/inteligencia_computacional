using BarbieWorld.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarbieWorld.Strategy
{
    public class WideSearch : SearchService
    {
        public WideSearch(string fileName)
        {
            SetStage(fileName);
        }

    }
}
