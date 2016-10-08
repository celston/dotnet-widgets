using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        Guid Revision { get; set; }
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
    }
}
