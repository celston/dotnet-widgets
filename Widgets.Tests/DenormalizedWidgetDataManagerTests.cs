using Microsoft.VisualStudio.TestTools.UnitTesting;

using Gnosis.Data;
using Gnosis.Entities.Data;
using Gnosis.Entities.Data.CommandBuilders.FieldVarchar;

using Widgets.Data;
using System.Data.Common;
using Gnosis.Entities.Data.CommandBuilders.Entity;
using Gnosis.Entities.Data.CommandBuilders.EntityRevision;

namespace Widgets.Tests
{
    [TestClass]
    public class DenormalizedWidgetDataManagerTests : WidgetDataManagerTests
    {
        ConfigurationConnectionBuilder connectionBuilder = new ConfigurationConnectionBuilder("WidgetsDenormalized");

        protected override IEntityDataManager BuildEntityDataManager()
        {
            return new EntityDataManager(connectionBuilder);
        }

        protected override IWidgetDataManager BuildWidgetDataManager()
        {
            return new DenormalizedWidgetDataManager();
        }

        protected override void ClearPreviousData()
        {
            FieldVarcharTruncateCommandBuilder fieldVarcharTruncateCommandBuilder = new FieldVarcharTruncateCommandBuilder();
            EntityTruncateCommandBuilder entityTruncateCommandBuilder = new EntityTruncateCommandBuilder();
            EntityRevisionTruncateCommandBuilder entityRevisionTruncateCommandBuilder = new EntityRevisionTruncateCommandBuilder();

            using (DbConnection conn = connectionBuilder.Build())
            {
                using (DbCommand cmd = fieldVarcharTruncateCommandBuilder.Build(conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (DbCommand cmd = entityTruncateCommandBuilder.Build(conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (DbCommand cmd = entityRevisionTruncateCommandBuilder.Build(conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
