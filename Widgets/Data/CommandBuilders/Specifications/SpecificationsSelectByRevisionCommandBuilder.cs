using Gnosis.Entities.Data.CommandBuilders;

namespace Widgets.Data.CommandBuilders.Specifications
{
    public class SpecificationsSelectByRevisionCommandBuilder : NormalizedDataSelectByRevisionCommandBuilder
    {
        protected override string TableName
        {
            get { return "Specifications"; }
        }
    }
}
