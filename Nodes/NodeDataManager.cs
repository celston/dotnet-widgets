using Gnosis.Data;
using Gnosis.Entities.Data;
using Gnosis.Entities.Data.CommandBuilders.Entity;
using Gnosis.Entities.Data.CommandBuilders.FieldUniqueIdentifier;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    public class NodeDataManager
    {
        #region Private Fields

        private IConnectionBuilder connectionBuilder;
        private EntityHydrator entityHydrator = new EntityHydrator();
        private NodeFactory factory = new NodeFactory();

        #endregion

        #region Constructors

        public NodeDataManager(IConnectionBuilder connectionBuilder)
        {
            this.connectionBuilder = connectionBuilder;
        }

        #endregion

        #region Public Methods

        public void CreateNode(Guid revision, INodeCreateRequest request)
        {
            using (DbConnection conn = BuildConnection())
            using (DbTransaction trans = conn.BeginTransaction())
            {
                EntityInsertCommandBuilder entityInsertCommandBuilder = new EntityInsertCommandBuilder();
                using (DbCommand cmd = entityInsertCommandBuilder.Build(conn, trans, request.Id, revision, "Node", request.Created))
                {
                    cmd.ExecuteNonQuery();
                }

                if (request.Parent.HasValue)
                {
                    FieldUniqueIdentifierInsertCommandBuilder fieldVarcharInsertCommandBuilder = new FieldUniqueIdentifierInsertCommandBuilder();
                    using (DbCommand cmd = fieldVarcharInsertCommandBuilder.Build(conn, trans, request.Id, revision, "Parent", 0, request.Parent.Value))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
            }
        }

        public Node RetrieveNode(Guid id)
        {
            Node entity = factory.Create();

            using (DbConnection conn = BuildConnection())
            {
                entityHydrator.Hydrate(conn, id, entity);

                NodeHydrator nodeHydrator = new NodeHydrator();
                nodeHydrator.Hydrate(conn, entity);

                PopulateChildren(conn, entity);
            }

            return entity;
        }

        #endregion

        #region Private Methods

        private void PopulateChildren(DbConnection conn, Node entity)
        {
            List<Node> children = new List<Node>();
            FieldUniqueIdentifierSelectActiveByNameValueCommandBuilder fieldUniqueIdentifierSelectActiveByNameValueCommandBuilder = new FieldUniqueIdentifierSelectActiveByNameValueCommandBuilder();
            using (DbCommand cmd = fieldUniqueIdentifierSelectActiveByNameValueCommandBuilder.Build(conn, "Parent", entity.Id))
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Node child = factory.Create();
                    entityHydrator.Hydrate(dr, child);
                    child.Parent = entity.Id;

                    children.Add(child);
                }
            }
            entity.Children = children;

            foreach (Node child in entity.Children)
            {
                PopulateChildren(conn, child);
            }
        }

        private DbConnection BuildConnection()
        {
            return connectionBuilder.Build();
        }

        #endregion
    }
}
