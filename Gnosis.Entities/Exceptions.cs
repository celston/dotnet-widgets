using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities
{
    public class EntityIdEmptyException : Exception
    {
    }

    public class EntityCreatedEmptyException : Exception
    {
    }

    public class EntityExistsException : Exception
    {
        public EntityExistsException(Guid id) : base(string.Format("Entity with ID {0} already exists", id))
        {
        }
    }

    public class EntityDoesntExistException : Exception
    {
        public EntityDoesntExistException(Guid? id) : this(id.Value)
        {
        }

        public EntityDoesntExistException(Guid id) : base(string.Format("Entity with ID {0} doesn't exist", id))
        {
        }
    }
}
