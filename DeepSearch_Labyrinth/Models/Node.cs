using System;
using System.Collections.Generic;
using System.Text;

namespace DeepSearch_Labyrinth.Models
{
    public class Node
    {
        private string state;
        private Node father;
        private int weight;

        public string State
        {
            get { return this.state; }
            set { this.state = value; }
        }

        public Node Father
        {
            get { return this.father; }
            set { this.father = value; }
        }

        public int Weight
        {
            get { return this.weight; }
            set { this.weight = value; }
        }
    }
}
