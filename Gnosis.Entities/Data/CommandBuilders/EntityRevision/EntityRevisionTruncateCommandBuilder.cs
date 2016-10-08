using Gnosis.Data.Commands;

namespace Gnosis.Entities.Data.CommandBuilders.EntityRevision
{
    public class EntityRevisionTruncateCommandBuilder : TruncateTableCommandBuilder
    {
        protected override string TableName
        {
            get
            {
                return "EntityRevision";
            }
        }
    }
}
