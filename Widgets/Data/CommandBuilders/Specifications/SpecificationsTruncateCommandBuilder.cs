using Gnosis.Data.Commands;

namespace Widgets.Data.CommandBuilders.Specifications
{
    public class SpecificationsTruncateCommandBuilder : TruncateTableCommandBuilder
    {
        protected override string TableName
        {
            get
            {
                return "Specifications";
            }
        }
    }
}
