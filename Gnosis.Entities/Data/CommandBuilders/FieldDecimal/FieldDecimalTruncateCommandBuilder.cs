using Gnosis.Data.Commands;

namespace Gnosis.Entities.Data.CommandBuilders.FieldDecimal
{
    public class FieldDecimalTruncateCommandBuilder : TruncateTableCommandBuilder
    {
        protected override string TableName
        {
            get
            {
                return "FieldDecimal";
            }
        }
    }
}
