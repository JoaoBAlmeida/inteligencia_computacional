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
        public override Node NodeRoot()
        {
            //Hard Coded starting position
            return new Node
            {
                Pos = new Position { Posx = 19, Posy = 23}
            };
        }

        //Convert WideSearch function here
    }
}
