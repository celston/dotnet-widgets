using System.Data.Common;

namespace Widgets.Data.CommandBuilders.Dimensions
{
    public interface IDimensionsInsertCommandBuilder
    {
        DbCommand Build(DbConnection conn, DbTransaction trans);
    }
}
