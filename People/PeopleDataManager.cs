using Gnosis.Data;
using Gnosis.Entities.Data;
using Gnosis.Entities.Data.CommandBuilders.Entity;
using Gnosis.Entities.Data.CommandBuilders.EntityRevision;
using Gnosis.Entities.Data.CommandBuilders.FieldVarchar;
using Gnosis.Entities.Data.CommandBuilders.FieldVarcharActive;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class PeopleDataManager
    {
        #region Private Fields

        private IConnectionBuilder connectionBuilder;

        #endregion

        #region Constructors

        public PeopleDataManager(IConnectionBuilder connectionBuilder)
        {
            this.connectionBuilder = connectionBuilder;
        }

        #endregion

        #region Public Methods

        public void CreatePerson(IPersonCreateRequest request, Guid revision, string type)
        {
            using (DbConnection conn = connectionBuilder.Build())
            using (DbTransaction trans = conn.BeginTransaction())
            {
                EntityDataCreator entityDataCreator = new EntityDataCreator();
                entityDataCreator.Execute(conn, trans, request, revision, type);

                FieldVarcharInsertCommandBuilder fieldVarcharInsertCommandBuilder = new FieldVarcharInsertCommandBuilder();
                using (DbCommand cmd = fieldVarcharInsertCommandBuilder.Build(conn, trans, request.Id, revision, "Birthdate", 0, request.Birthdate))
                {
                    cmd.ExecuteNonQuery();
                }

                FieldVarcharActiveInsertCommandBuilder fieldVarcharActiveInsertCommandBuilder = new FieldVarcharActiveInsertCommandBuilder();
                using (DbCommand cmd = fieldVarcharActiveInsertCommandBuilder.Build(conn, trans, request.Id, "Birthdate", 0, request.Birthdate))
                {
                    cmd.ExecuteNonQuery();
                }

                trans.Commit();
            }
        }

        #endregion
    }
}
