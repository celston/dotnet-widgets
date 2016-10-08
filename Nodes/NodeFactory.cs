using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    public class NodeFactory
    {
        public Node Create()
        {
            return new Node();
        }
    }
}
