using System;

using Gnosis.Entities.Data;

namespace Gnosis.Entities.Validators
{
    public class EntityUpdateValidator
    {
        public void Validate(IEntityDataManager dataManager, IEntityUpdateRequest request)
        {
            if (request.Id == Guid.Empty)
            {
                throw new Exception("ID cannot be empty");
            }
            if (request.Revision == Guid.Empty)
            {
                throw new Exception("Revision cannot be empty");
            }
            if (request.Updated == DateTime.MinValue)
            {
                throw new Exception("Updated cannot be empty");
            }
            if (dataManager.GetEntityCountByIdAndRevision(request.Id, request.Revision) == 0)
            {
                throw new Exception(string.Format("Invalid revision ({0}) for entity ({1})", request.Revision, request.Id));
            }
        }
    }
}
