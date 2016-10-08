using Gnosis.Data.Commands;

namespace Widgets.Data.CommandBuilders.Dimensions
{
    public class StoredProcedureDimensionsInsertCommandBuilder : StoredProcdureCommandBuilder, IDimensionsInsertCommandBuilder
    {
        public StoredProcedureDimensionsInsertCommandBuilder() : base("Dimensions_Insert")
        {
        }
    }
}
