using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BarbieWorld.Models
{
    public class MapCell
    {
        public Color Color { get; set; }
        public bool Habitable { get; set; }
        public int Weight { get; set; }
        public Position Pos { get; set; }
        public List<MapCell> Children { get; set; }
        public override string ToString()
        {

            return "Color: " + Color + "\nWeight: " + Weight + "\n(Posx,PosY): (" + Pos.Posx + "," + Pos.Posy + ")\n=====================";
        }
    }
}
