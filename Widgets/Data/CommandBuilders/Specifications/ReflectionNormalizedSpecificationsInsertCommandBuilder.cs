using Gnosis.Entities.Data.CommandBuilders;

namespace Widgets.Data.CommandBuilders.Specifications
{
    public class ReflectionNormalizedSpecificationsInsertCommandBuilder : ReflectionNormalizedDataInsertCommandBuilder<ISpecifications>, ISpecificationsInsertCommandBuilder
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
