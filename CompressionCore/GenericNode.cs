using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionCore
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
