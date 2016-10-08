using System;
using System.Data.Common;

using Gnosis.Data;

using Gnosis.Entities.Data.CommandBuilders.Entity;

namespace Gnosis.Entities.Data
{
    public class EntityDataManager : IEntityDataManager
    {
        #region Private Fields

        private IConnectionBuilder connectionBuilder;
        private EntitySelectCountByIdAndRevisionCommandBuilder entitySelectCountByIdAndRevisionCommandBuilder = new EntitySelectCountByIdAndRevisionCommandBuilder();

        #endregion

        #region Constructors

        public EntityDataManager(string connectionStringSettingsName)
        {
            connectionBuilder = new ConfigurationConnectionBuilder(connectionStringSettingsName);
        }

        public EntityDataManager(IConnectionBuilder connectionBuilder)
        {
            this.connectionBuilder = connectionBuilder;
        }

        #endregion

        #region IEntityDataManager Implementation

        public int GetEntityCountByIdAndRevision(Guid id, Guid revision)
        {
            using (DbConnection conn = connectionBuilder.Build())
            using (DbCommand cmd = entitySelectCountByIdAndRevisionCommandBuilder.Build(conn, id, revision))
            {
                return (int)cmd.ExecuteScalar();
            }
        }

        public int GetEntityCountById(Guid id)
        {
            EntityCountByIdCommandBuilder commandBuilder = new EntityCountByIdCommandBuilder();

            using (DbConnection conn = connectionBuilder.Build())
            using (DbCommand cmd = commandBuilder.Build(conn, id))
            {
                return (int)cmd.ExecuteScalar();
            }
        }

        public int GetEntityCountById(Guid? id)
        {
            return GetEntityCountById(id.Value);
        }

        #endregion
    }
}
