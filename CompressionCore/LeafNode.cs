using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionCore
{
    public class LeafNode : GenericNode
    {
        public byte Value { get; private set; }

        public LeafNode() : base() { }

        public LeafNode(byte value, int weight)
            : base(weight)
        {
            Value = value;
        }
    }
}
