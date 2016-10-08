using System.Data;
using System.Data.Common;

namespace Gnosis.Data.Commands
{
    public abstract class CommandBuilder
    {
        #region Protected Fields

        protected ParameterHelper parameterHelper = new ParameterHelper();

        #endregion

        #region Protected Methods

        protected DbCommand BuildTextCommand(DbConnection conn)
        {
            DbCommand cmd = conn.CreateCommand();

            cmd.CommandType = CommandType.Text;

            return cmd;
        }

        protected DbCommand BuildTextCommand(DbConnection conn, DbTransaction trans)
        {
            DbCommand cmd = conn.CreateCommand();

            cmd.CommandType = CommandType.Text;
            cmd.Transaction = trans;

            return cmd;
        }



        #endregion
    }
}
