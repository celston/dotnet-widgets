using Gnosis.Data.Commands;

namespace Gnosis.Entities.Data.CommandBuilders.FieldDecimal
{
    public class StoredProcedureFieldDecimalInsertCommandBuilder : StoredProcdureCommandBuilder, IFieldDecimalInsertCommandBuilder
    {
        public StoredProcedureFieldDecimalInsertCommandBuilder() : base("FieldDecimal_Insert")
        {
        }
    }
}
