using System.Data.Common;

namespace Gnosis.Entities.Data.CommandBuilders.FieldDecimal
{
    public interface IFieldDecimalInsertCommandBuilder
    {
        DbCommand Build(DbConnection conn);
    }
}
