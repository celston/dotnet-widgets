using Gnosis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public interface IPersonCreateRequest : IEntityCreateRequest, IPerson
    {
        Guid? Mother { get; }
        Guid? Father { get; }
    }
}
