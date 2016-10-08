using System;
using System.Collections.Generic;
using System.Data.Common;

using Gnosis.Data;

using Gnosis.Entities.Data.CommandBuilders.FieldVarchar;
using Gnosis.Entities.Data.CommandBuilders.Entity;

namespace Widgets.Data
{
    public class DenormalizedWidgetDataManager : WidgetDataManager, IWidgetDataManager
    {
        #region Private Fields

        private FieldVarcharInsertCommandBuilder fieldVarcharInsertCommandBuilder = new FieldVarcharInsertCommandBuilder();
        private FieldVarcharSelectByRevisionCommandBuilder fieldVarcharSelectByRevisionCommandBuilder = new FieldVarcharSelectByRevisionCommandBuilder();

        #endregion

        #region Constructors

        public DenormalizedWidgetDataManager()
            : base(new ConfigurationConnectionBuilder("WidgetsDenormalized"))
        {
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

                InsertFields(conn, trans, request.Id, revision, request);

                trans.Commit();
            }
        }

        public void HydrateWidget(Guid id, Widget widget)
        {
            using (DbConnection conn = BuildConnection())
            {
                HydrateEntity(conn, id, widget);

                using (DbCommand cmd = fieldVarcharSelectByRevisionCommandBuilder.Build(conn, widget.Revision))
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    Dictionary<string, string> values = new Dictionary<string, string>();
                    while (dr.Read())
                    {
                        values.Add((string)dr["name"], (string)dr["value"]);
                    }

                    specificationsHydrator.Hydrate(values, widget);
                    dimensionsHydrator.Hydrate(values, widget);
                    datesHydrator.Hydrate(values, widget);
                }
            }
        }

        public ICollection<Widget> RetrieveAllWidgets()
        {
            EntitySelectByTypeCommandBuilder entityCommandBuilder = new EntitySelectByTypeCommandBuilder();
            FieldVarcharSelectActiveCommandBuilder fieldCommandBuilder = new FieldVarcharSelectActiveCommandBuilder();
            Dictionary<Guid, Widget> result = new Dictionary<Guid, Widget>();

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

                Dictionary<Guid, Dictionary<string, string>> allFields = new Dictionary<Guid, Dictionary<string, string>>();

                using (DbCommand cmd = fieldCommandBuilder.Build(conn))
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Guid id = (Guid)dr["id"];
                        string name = (string)dr["name"];
                        string value = (string)dr["value"];

                        if (!allFields.ContainsKey(id))
                        {
                            allFields.Add(id, new Dictionary<string, string>());
                        }
                        allFields[id].Add(name, value);
                    }
                }

                foreach (KeyValuePair<Guid, Dictionary<string, string>> fields in allFields)
                {
                    dimensionsHydrator.Hydrate(fields.Value, result[fields.Key]);
                    specificationsHydrator.Hydrate(fields.Value, result[fields.Key]);
                }
            }

            return result.Values;
        }

        public void UpdateWidget(Guid revision, IWidgetUpdateRequest request)
        {
            using (DbConnection conn = BuildConnection())
            using (DbTransaction trans = conn.BeginTransaction())
            {
                UseAndExecuteNonQuery(entityUpdateCommandBuilder.Build(conn, trans, request.Id, revision, request.Updated));
                UseAndExecuteNonQuery(entityRevisionInsertCommandBuilder.Build(conn, trans, request.Id, revision, request.Updated));

                InsertFields(conn, trans, request.Id, revision, request);

                trans.Commit();
            }
        }

        #endregion

        private void InsertFields(DbConnection conn, DbTransaction trans, Guid id, Guid revision, IWidget request)
        {
            UseAndExecuteNonQuery(fieldVarcharInsertCommandBuilder.Build(conn, trans, id, revision, "Height", 0, request.Height));
            UseAndExecuteNonQuery(fieldVarcharInsertCommandBuilder.Build(conn, trans, id, revision, "Length", 0, request.Length));
            UseAndExecuteNonQuery(fieldVarcharInsertCommandBuilder.Build(conn, trans, id, revision, "Width", 0, request.Width));

            UseAndExecuteNonQuery(fieldVarcharInsertCommandBuilder.Build(conn, trans, id, revision, "Weight", 0, request.Weight));
            UseAndExecuteNonQuery(fieldVarcharInsertCommandBuilder.Build(conn, trans, id, revision, "Volume", 0, request.Volume));

            if (request.ReleaseDate.HasValue)
            {
                UseAndExecuteNonQuery(fieldVarcharInsertCommandBuilder.Build(conn, trans, id, revision, "ReleaseDate", 0, request.ReleaseDate));
            }
        }
    }
}
