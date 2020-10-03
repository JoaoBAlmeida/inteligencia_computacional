using System;
using System.Collections.Generic;
using System.Text;

namespace BarbieWorld.Models
{
    public class Node
    {
        private Position pos;
        private Node father;

        public Position Pos
        {
            get { return this.pos; }
            set { this.pos = value; }
        }

        public Node Father
        {
            get { return this.father; }
            set { this.father = value; }
        }
    }
}
