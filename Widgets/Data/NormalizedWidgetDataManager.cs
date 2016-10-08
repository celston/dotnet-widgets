using System;
using System.Data.Common;

using Gnosis.Data;
using Widgets.Data.Hydrators;
using System.Collections.Generic;
using Gnosis.Entities.Data.CommandBuilders.Entity;
using Widgets.Data.CommandBuilders.Dimensions;
using Widgets.Data.CommandBuilders.Specifications;
using Widgets.Data.CommandBuilders.Dates;

namespace Widgets.Data
{
    public class NormalizedWidgetDataManager : WidgetDataManager, IWidgetDataManager
    {
        #region Private Fields

        private ISpecificationsInsertCommandBuilder specificationsInsertCommandBuilder;
        private DimensionsInsertCommandBuilder dimensionsInsertCommandBuilder = new DimensionsInsertCommandBuilder();
        private DatesInsertCommandBuilder datesInsertCommandBuilder = new DatesInsertCommandBuilder();

        #endregion

        #region Constructors

        public NormalizedWidgetDataManager(ISpecificationsInsertCommandBuilder specificationsInsertCommandBuilder)
            : base(new ConfigurationConnectionBuilder("WidgetsNormalized"))
        {
            this.specificationsInsertCommandBuilder = specificationsInsertCommandBuilder;
        }

        #endregion

        #region IWidgetDataManager Implementation

        public void CreateWidget(Guid revision, IWidgetCreateRequest request)
        {
            using (DbConnection conn = BuildConnection())
            using (DbTransaction trans = conn.BeginTransaction())
            {
                UseAndExecuteNonQuery(entityInsertCommandBuilder.Build(conn, trans, request.Id, revision, "Widget", request.Created));
                UseAndExecuteNonQuery(entityRevisionInsertCommandBuilder.Build(conn, trans, request.Id, revision, request.Created));
                UseAndExecuteNonQuery(dimensionsInsertCommandBuilder.Build(conn, trans, request.Id, revision, request));
                UseAndExecuteNonQuery(specificationsInsertCommandBuilder.Build(conn, trans, request.Id, revision, request));
                UseAndExecuteNonQuery(datesInsertCommandBuilder.Build(conn, trans, request.Id, revision, request));

                trans.Commit();
            }
        }

        public void UpdateWidget(Guid revision, IWidgetUpdateRequest request)
        {
            using (DbConnection conn = BuildConnection())
            using (DbTransaction trans = conn.BeginTransaction())
            {
                UseAndExecuteNonQuery(entityUpdateCommandBuilder.Build(conn, trans, request.Id, revision, request.Updated));
                UseAndExecuteNonQuery(entityRevisionInsertCommandBuilder.Build(conn, trans, request.Id, revision, request.Updated));
                UseAndExecuteNonQuery(dimensionsInsertCommandBuilder.Build(conn, trans, request.Id, revision, request));
                UseAndExecuteNonQuery(specificationsInsertCommandBuilder.Build(conn, trans, request.Id, revision, request));
                UseAndExecuteNonQuery(datesInsertCommandBuilder.Build(conn, trans, request.Id, revision, request));

                trans.Commit();
            }
        }

        public void HydrateWidget(Guid id, Widget widget)
        {
            using (DbConnection conn = BuildConnection())
            {
                HydrateEntity(conn, id, widget);
                HydrateDimensions(conn, widget.Revision, widget);
                HydrateSpecifications(conn, widget.Revision, widget);
                HydrateDates(conn, widget.Revision, widget);
            }
        }

        public ICollection<Widget> RetrieveAllWidgets()
        {
            Dictionary<Guid, Widget> result = new Dictionary<Guid, Widget>();

            EntitySelectByTypeCommandBuilder entityCommandBuilder = new EntitySelectByTypeCommandBuilder();
            DimensionsSelectActiveCommandBuilder dimensionsSelectActiveCommandBuilder = new DimensionsSelectActiveCommandBuilder();
            SpecificationsSelectActiveCommandBuilder specificationsSelectActiveCommandBuilder = new SpecificationsSelectActiveCommandBuilder();
            
            using (DbConnection conn = BuildConnection())
            {
                using (DbCommand cmd = entityCommandBuilder.Build(conn, "Widget"))
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Widget entity = new Widget();
                        entityHydrator.Hydrate(dr, entity);
                        result.Add(entity.Id, entity);
                    }
                }

                using (DbCommand cmd = specificationsSelectActiveCommandBuilder.Build(conn))
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Guid id = (Guid)dr["id"];
                        specificationsHydrator.Hydrate(dr, result[id]);
                    }
                }

                using (DbCommand cmd = dimensionsSelectActiveCommandBuilder.Build(conn))
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Guid id = (Guid)dr["id"];
                        dimensionsHydrator.Hydrate(dr, result[id]);
                    }
                }
            }

            return result.Values;
        }

        #endregion

        #region Protected Methods

        protected void HydrateSpecifications(DbConnection conn, Guid revision, ISpecifications specifications)
        {
            SpecificationsSelectByRevisionCommandBuilder cmdBuilder = new SpecificationsSelectByRevisionCommandBuilder();

            using (DbCommand cmd = cmdBuilder.Build(conn, revision))
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    specificationsHydrator.Hydrate(dr, specifications);
                }
            }
        }

        protected void HydrateDimensions(DbConnection conn, Guid revision, IDimensions dimensions)
        {
            DimensionsSelectByRevisionCommandBuilder cmdBuilder = new DimensionsSelectByRevisionCommandBuilder();

            using (DbCommand cmd = cmdBuilder.Build(conn, revision))
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    dimensionsHydrator.Hydrate(dr, dimensions);
                }
            }
        }

        protected void HydrateDates(DbConnection conn, Guid revision, IDates data)
        {
            DatesSelectByRevisionCommandBuilder cmdBuilder = new DatesSelectByRevisionCommandBuilder();

            using (DbCommand cmd = cmdBuilder.Build(conn, revision))
            using (DbDataReader dr = cmd.ExecuteReader())
            {
                if (dr.Read())
                {
                    datesHydrator.Hydrate(dr, data);
                }
            }
        }

        #endregion
    }
}
