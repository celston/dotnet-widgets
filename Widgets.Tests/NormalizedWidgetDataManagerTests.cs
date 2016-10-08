using Gnosis.Entities.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Widgets.Data;
using Gnosis.Data;
using System.Data.Common;
using Gnosis.Entities.Data.CommandBuilders.EntityRevision;
using Gnosis.Entities.Data.CommandBuilders.Entity;
using Widgets.Data.CommandBuilders.Dimensions;
using Widgets.Data.CommandBuilders.Specifications;
using Widgets.Data.CommandBuilders.Dates;

namespace Widgets.Tests
{
    [TestClass]
    public class NormalizedWidgetDataManagerTests : WidgetDataManagerTests
    {
        ConfigurationConnectionBuilder connectionBuilder = new ConfigurationConnectionBuilder("WidgetsNormalized");

        protected override IEntityDataManager BuildEntityDataManager()
        {
            return new EntityDataManager(connectionBuilder);
        }

        protected override IWidgetDataManager BuildWidgetDataManager()
        {
            ISpecificationsInsertCommandBuilder specificationsInsertCommandBuilder = new ReflectionNormalizedSpecificationsInsertCommandBuilder();
            return new NormalizedWidgetDataManager(specificationsInsertCommandBuilder);
        }

        protected override void ClearPreviousData()
        {
            EntityTruncateCommandBuilder entityTruncateCommandBuilder = new EntityTruncateCommandBuilder();
            EntityRevisionTruncateCommandBuilder entityRevisionTruncateCommandBuilder = new EntityRevisionTruncateCommandBuilder();
            SpecificationsTruncateCommandBuilder specificationsTruncateCommandBuilder = new SpecificationsTruncateCommandBuilder();
            DimensionsTruncateCommandBuilder dimensionsTruncateCommandBuilder = new DimensionsTruncateCommandBuilder();
            DatesTruncateCommandBuilder datesTruncateCommandBuilder = new DatesTruncateCommandBuilder();

            using (DbConnection conn = connectionBuilder.Build())
            {
                using (DbCommand cmd = entityTruncateCommandBuilder.Build(conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (DbCommand cmd = entityRevisionTruncateCommandBuilder.Build(conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (DbCommand cmd = specificationsTruncateCommandBuilder.Build(conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (DbCommand cmd = dimensionsTruncateCommandBuilder.Build(conn))
                {
                    cmd.ExecuteNonQuery();
                }

                using (DbCommand cmd = datesTruncateCommandBuilder.Build(conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
