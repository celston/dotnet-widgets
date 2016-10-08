using Gnosis.Entities.Data.CommandBuilders.Entity;
using Gnosis.Entities.Data.CommandBuilders.EntityRevision;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Entities.Data
{
    public class EntityDataCreator
    {
        public void Execute(DbConnection conn, DbTransaction trans, IEntityCreateRequest request, Guid revision, string type)
        {
            EntityInsertCommandBuilder entityInsertCommandBuilder = new EntityInsertCommandBuilder();
            using (DbCommand cmd = entityInsertCommandBuilder.Build(conn, trans, request.Id, revision, type, request.Created))
            {
                cmd.ExecuteNonQuery();
            }

            EntityRevisionInsertCommandBuilder entityRevisionInsertCommandBuilder = new EntityRevisionInsertCommandBuilder();
            using (DbCommand cmd = entityRevisionInsertCommandBuilder.Build(conn, trans, request.Id, revision, request.Created))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
