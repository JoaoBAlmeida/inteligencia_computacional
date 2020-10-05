using System;
using System.Collections.Generic;
using System.Text;

namespace BarbieWorld.Models
{
    public class NPC
    {
        public bool Ansr { get; set; }
        public Position Pos { get; set; }

        public override string ToString()
        {
            return "Answer: " + Ansr + "\n(Posx,Posy): (" + Pos.Posx + "," + Pos.Posy + ")\n===============";
        }
    }
}
