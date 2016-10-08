using Gnosis.Data;
using Gnosis.Entities.Data;
using Gnosis.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    public class NodeCreateValidator
    {
        public void Validate(IEntityDataManager entityDataManager, INodeCreateRequest request)
        {
            if (request.Parent.HasValue)
            {
                if (entityDataManager.GetEntityCountById(request.Parent) == 0)
                {
                    throw new EntityDoesntExistException(request.Parent);
                }
            }
        }
    }
}
