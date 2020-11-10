using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation
{
    public class NonLeafNode: GenericNode
    {
        public GenericNode Right { get; private set; }
        
        public GenericNode Left { get; private set; }

        public NonLeafNode(GenericNode left, GenericNode right)
            : base(left.Weight + right.Weight)
        {
            Left = left;
            Right = right;
        }
    }
}
