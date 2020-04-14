using System;
using System.Collections.Generic;
using System.Text;

namespace DijkstraAlgo
{
    public class Node
    {
        public string Name { get; set; }
        public double Costs { get; set; }
        public Node Parent { get; set; }
    }
}
