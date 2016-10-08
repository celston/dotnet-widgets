using Gnosis.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Validators
{
    public class EntityRetrieveValidator
    {
        public void Validate(IEntityDataManager dataManager, Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new EntityIdEmptyException();
            }
            if (dataManager.GetEntityCountById(id) == 0)
            {
                throw new EntityDoesntExistException(id);
            }
        }
    }
}
