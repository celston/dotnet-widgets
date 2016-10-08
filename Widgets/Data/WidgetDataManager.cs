using System;
using System.Data.Common;

using Gnosis.Data;

using Gnosis.Entities;
using Gnosis.Entities.Data;
using Gnosis.Entities.Data.CommandBuilders.Entity;
using Gnosis.Entities.Data.CommandBuilders.EntityRevision;

using Widgets.Data.Hydrators;

namespace Widgets.Data
{
    public abstract class WidgetDataManager
    {
        #region Protected Fields

        protected ConfigurationConnectionBuilder connectionBuilder;
        protected EntityInsertCommandBuilder entityInsertCommandBuilder = new EntityInsertCommandBuilder();
        protected EntityRevisionInsertCommandBuilder entityRevisionInsertCommandBuilder = new EntityRevisionInsertCommandBuilder();
        protected EntityUpdateCommandBuilder entityUpdateCommandBuilder = new EntityUpdateCommandBuilder();
        protected EntityHydrator entityHydrator = new EntityHydrator();
        protected EntitySelectByIdCommandBuilder entitySelectByIdCommandBuilder = new EntitySelectByIdCommandBuilder();
        protected SpecificationsHydrator specificationsHydrator = new SpecificationsHydrator();
        protected DimensionsHydrator dimensionsHydrator = new DimensionsHydrator();
        protected DatesHydrator datesHydrator = new DatesHydrator();

        #endregion

        #region Constructors

        protected WidgetDataManager(ConfigurationConnectionBuilder connectionBuilder)
        {
            this.connectionBuilder = connectionBuilder;
        }

        #endregion

        #region Protected Methods

        protected DbConnection BuildConnection()
        {
            return connectionBuilder.Build();
        }

        protected void UseAndExecuteNonQuery(DbCommand cmd)
        {
            using (cmd)
            {
                cmd.ExecuteNonQuery();
            }
        }

        protected void HydrateEntity(DbConnection conn, Guid id, IEntity entity)
        {
            using (DbCommand cmd = entitySelectByIdCommandBuilder.Build(conn, id))
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    entityHydrator.Hydrate(dr, entity);
                }
            }
        }

        #endregion
    }
}
