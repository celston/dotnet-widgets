using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data
{
    public interface IEntityDataManager
    {
        int GetEntityCountByIdAndRevision(Guid id, Guid revision);
        int GetEntityCountById(Guid id);
        int GetEntityCountById(Guid? id);
    }
}
