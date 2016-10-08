using Gnosis.Data.Commands;

namespace Widgets.Data.CommandBuilders.Dates
{
    public class DatesTruncateCommandBuilder : TruncateTableCommandBuilder
    {
        protected override string TableName
        {
            get
            {
                return "Dates";
            }
        }
    }
}
