using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Data.Commands
{
    public abstract class InsertCommandBuilder : CommandBuilder
    {
        #region Protected Abstract Properties

        protected abstract string TableName { get; }
        protected abstract IEnumerable<string> Columns { get; }

        #endregion

        #region Protected Virtual Methods

        protected virtual DbCommand DoBuild(DbConnection conn)
        {
            DbCommand cmd = BuildTextCommand(conn);
            
            string columnsList = string.Join(",", Columns);
            string parametersList = string.Join(",", Columns.Select(x => "@" + x));

            cmd.CommandText = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", TableName, columnsList, parametersList);

            return cmd;
        }

        protected virtual DbCommand DoBuild(DbConnection conn, DbTransaction trans)
        {
            DbCommand cmd = DoBuild(conn);
            cmd.Transaction = trans;

            return cmd;
        }

        #endregion
    }
}
