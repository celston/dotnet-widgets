using System;

using Gnosis.Entities.Data;

namespace Gnosis.Entities.Validators
{
    public class EntityCreateValidator
    {
        public void Validate(IEntityDataManager dataManager, IEntityCreateRequest request)
        {
            if (request.Id == Guid.Empty)
            {
                throw new EntityIdEmptyException();
            }
            if (request.Created == DateTime.MinValue)
            {
                throw new EntityCreatedEmptyException();
            }
            if (dataManager.GetEntityCountById(request.Id) > 0)
            {
                throw new EntityExistsException(request.Id);
            }
        }
    }
}
