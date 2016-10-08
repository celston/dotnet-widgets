using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities
{
    public abstract class Entity : IEntity
    {
        public DateTime Created { get; set; }
        public Guid Id { get; set; }
        public Guid Revision { get; set; }
        public DateTime Updated { get; set; }
    }
}
