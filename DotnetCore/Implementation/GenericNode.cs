using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation
{
    public class GenericNode
    {
        public int Weight { get; protected set; }
        
        public GenericNode(int weight)
        {
            Weight = weight;
        }

        public GenericNode() : this(1) { }
    }
}
