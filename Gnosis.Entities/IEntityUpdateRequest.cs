using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities
{
    public interface IEntityUpdateRequest
    {
        Guid Id { get; }
        Guid Revision { get; }
        DateTime Updated { get; }
    }
}
