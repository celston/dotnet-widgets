using System;
using System.Data;

using Gnosis.Data.Hydrators;
using Gnosis.Entities.Data.CommandBuilders.Entity;
using System.Data.Common;

namespace Gnosis.Entities.Data
{
    public class EntityHydrator : Hydrator
    {
        public void Hydrate(DbConnection conn, Guid id, IEntity entity)
        {
            EntitySelectByIdCommandBuilder entitySelectByIdCommandBuilder = new EntitySelectByIdCommandBuilder();
            using (DbCommand cmd = entitySelectByIdCommandBuilder.Build(conn, id))
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    Hydrate(dr, entity);
                }
            }
        }

        public void Hydrate(IDataReader dr, IEntity entity)
        {
            entity.Id = GetGuid(dr, "id");
            entity.Revision = GetGuid(dr, "revision");
            entity.Created = GetDateTime(dr, "created");
            entity.Updated = GetDateTime(dr, "updated");
        }
    }
}
