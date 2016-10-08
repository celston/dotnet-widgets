using System;
using System.Collections.Generic;
using System.Data.Common;

using Gnosis.Data.Hydrators;
using Gnosis.Entities.Data.CommandBuilders.FieldUniqueIdentifier;

namespace Nodes
{
    public class NodeHydrator : Hydrator
    {
        public void Hydrate(DbDataReader dr, Node output)
        {
            switch ((string)dr["name"])
            {
                case "Parent":
                    output.Parent = (Guid)dr["value"];
                    break;
            }
        }

        public void Hydrate(DbConnection conn, Node output)
        {
            FieldUniqueIdentifierSelectByRevisionCommandBuilder fieldUniqueIdentifierSelectActiveCommandBuilder = new FieldUniqueIdentifierSelectByRevisionCommandBuilder();
            using (DbCommand cmd = fieldUniqueIdentifierSelectActiveCommandBuilder.Build(conn, output.Revision))
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Hydrate(dr, output);
                }
            }
        }
    }
}
