using System;
using System.Collections.Generic;

using Gnosis.Entities;

namespace Nodes
{
    public class Node : Entity
    {
        internal Node()
        {
        }

        public Guid? Parent { get; set; }
        public ICollection<Node> Children { get; set; }
    }
}
