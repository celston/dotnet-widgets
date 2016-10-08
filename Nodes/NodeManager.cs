using Gnosis.Data;
using Gnosis.Entities.Data;
using Gnosis.Entities.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    public class NodeManager
    {
        IConnectionBuilder connectionBuilder = new ConfigurationConnectionBuilder("WidgetsDenormalized");
        EntityDataManager entityDataManager;
        NodeDataManager nodeDataManager;

        public NodeManager()
        {
            entityDataManager = new EntityDataManager(connectionBuilder);
            nodeDataManager = new NodeDataManager(connectionBuilder);
        }

        public Guid CreateNode(INodeCreateRequest request)
        {
            EntityCreateValidator entityCreateValidator = new EntityCreateValidator();
            entityCreateValidator.Validate(entityDataManager, request);

            NodeCreateValidator nodeCreateValidator = new NodeCreateValidator();
            nodeCreateValidator.Validate(entityDataManager, request);

            Guid revision = Guid.NewGuid();

            nodeDataManager.CreateNode(revision, request);

            return revision;
        }

        public Node RetrieveNode(Guid id)
        {
            EntityRetrieveValidator entityRetrieveValidator = new EntityRetrieveValidator();
            entityRetrieveValidator.Validate(entityDataManager, id);

            return nodeDataManager.RetrieveNode(id);
        }
    }
}
