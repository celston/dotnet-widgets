using Gnosis.Entities;
using System;

namespace Nodes
{
    public interface INodeCreateRequest : IEntityCreateRequest
    {
        Guid? Parent { get; }
    }
}
