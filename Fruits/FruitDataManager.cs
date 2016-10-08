using System;
using System.Data.Common;

using Gnosis.Data;

using Gnosis.Entities.Data.CommandBuilders.Entity;
using Gnosis.Entities.Data;
using System.Collections.Generic;
using Fruits.Oranges;
using Fruits.Bananas;
using Gnosis.Entities.Data.CommandBuilders.FieldVarchar;
using Gnosis.Entities.Data.CommandBuilders.FieldVarcharActive;
using Gnosis.Entities.Data.CommandBuilders.EntityRevision;

namespace Fruits
{
    public class FruitDataManager
    {
        #region Private Fields

        private IConnectionBuilder connectionBuilder;

        #endregion

        #region Constructors

        public FruitDataManager(IConnectionBuilder connectionBuilder)
        {
            this.connectionBuilder = connectionBuilder;
        }

        #endregion

        #region Public Methods

        public void CreateFruit(string type, Guid revision, IFruitCreateRequest request)
        {
            using (DbConnection conn = BuildConnection())
            using (DbTransaction trans = conn.BeginTransaction())
            {
                EntityDataCreator entityDataCreator = new EntityDataCreator();
                entityDataCreator.Execute(conn, trans, request, revision, type);

                FieldVarcharInsertCommandBuilder fieldVarcharInsertCommandBuilder = new FieldVarcharInsertCommandBuilder();
                using (DbCommand cmd = fieldVarcharInsertCommandBuilder.Build(conn, trans, request.Id, revision, "Weight", 0, request.Weight))
                {
                    cmd.ExecuteNonQuery();
                }

                FieldVarcharActiveInsertCommandBuilder fieldVarcharActiveInsertCommandBuilder = new FieldVarcharActiveInsertCommandBuilder();
                using (DbCommand cmd = fieldVarcharActiveInsertCommandBuilder.Build(conn, trans, request.Id, "Weight", 0, request.Weight))
                {
                    cmd.ExecuteNonQuery();
                }

                trans.Commit();
            }
        }

        public void HydrateFruit(Guid id, string type, Fruit entity)
        {
            FieldVarcharActiveSelectByIdCommandBuilder fieldVarcharActiveSelectByIdCommandBuilder = new FieldVarcharActiveSelectByIdCommandBuilder();

            using (DbConnection conn = BuildConnection())
            {
                EntitySelectByIdAndTypeCommandBuilder entitySelectByIdCommandBuilder = new EntitySelectByIdAndTypeCommandBuilder();
                using (DbCommand cmd = entitySelectByIdCommandBuilder.Build(conn, id, type))
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        EntityHydrator entityHydrator = new EntityHydrator();
                        entityHydrator.Hydrate(dr, entity);
                    }
                }

                using (DbCommand cmd = fieldVarcharActiveSelectByIdCommandBuilder.Build(conn, entity.Id))
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    Dictionary<string, string> values = new Dictionary<string, string>();
                    while (dr.Read())
                    {
                        values.Add((string)dr["name"], (string)dr["value"]);
                    }

                    FruitHydrator fruitHydrator = new FruitHydrator();
                    fruitHydrator.Hydrate(values, entity);
                }
            }
        }

        public void UpdateFruit(Guid revision, IFruitUpdateRequest request)
        {
            using (DbConnection conn = BuildConnection())
            using (DbTransaction trans = conn.BeginTransaction())
            {
                EntityUpdateCommandBuilder entityUpdateCommandBuilder = new EntityUpdateCommandBuilder();
                using (DbCommand cmd = entityUpdateCommandBuilder.Build(conn, trans, request.Id, revision, request.Updated))
                {
                    cmd.ExecuteNonQuery();
                }

                EntityRevisionInsertCommandBuilder entityRevisionInsertCommandBuilder = new EntityRevisionInsertCommandBuilder();
                using (DbCommand cmd = entityRevisionInsertCommandBuilder.Build(conn, trans, request.Id, revision, request.Updated))
                {
                    cmd.ExecuteNonQuery();
                }

                FieldVarcharInsertCommandBuilder fieldVarcharInsertCommandBuilder = new FieldVarcharInsertCommandBuilder();
                using (DbCommand cmd = fieldVarcharInsertCommandBuilder.Build(conn, trans, request.Id, revision, "Weight", 0, request.Weight))
                {
                    cmd.ExecuteNonQuery();
                }

                FieldVarcharActiveUpdateCommandBuilder fieldVarcharActiveUpdateCommandBuilder = new FieldVarcharActiveUpdateCommandBuilder();
                using (DbCommand cmd = fieldVarcharActiveUpdateCommandBuilder.Build(conn, trans, request.Id, "Weight", 0, request.Weight))
                {
                    cmd.ExecuteNonQuery();
                }

                trans.Commit();
            }
        }

        public ICollection<Fruit> RetrieveAllFruit()
        {
            List<Fruit> result = new List<Fruit>();

            using (DbConnection conn = BuildConnection())
            {
                EntitySelectCommandBuilder entitySelectCommandBuilder = new EntitySelectCommandBuilder();
                EntityHydrator entityHydrator = new EntityHydrator();

                using (DbCommand cmd = entitySelectCommandBuilder.Build(conn))
                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string type = (string)dr["type"];
                        Fruit entity = null;
                        switch (type)
                        {
                            case "ValenciaOrange":
                                entity = new ValenciaOrange();
                                break;

                            case "CavendishBanana":
                                entity = new CavendishBanana();
                                break;

                            default:
                                throw new Exception(string.Format("Unknown fruit entity type: {0}", type));
                        }

                        entityHydrator.Hydrate(dr, entity);
                        result.Add(entity);
                    }
                }
            }

            return result;
        }

        #endregion

        #region Private Methods

        private DbConnection BuildConnection()
        {
            return connectionBuilder.Build();
        }

        #endregion
    }
}
