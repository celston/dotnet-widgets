using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;

using Moq;

using Gnosis.Data;

using Gnosis.Entities;
using Gnosis.Entities.Data.CommandBuilders.FieldDecimal;

using Widgets.Data.CommandBuilders;
using Widgets.Data.CommandBuilders.Dimensions;

namespace Widgets.Tests
{
    [TestClass]
    public class FooTests
    {
        int repeats = 1;

        [TestMethod]
        public void TextDimensionsInsertCommandBuilder_Success()
        {
            using (DbConnection conn = CreateNormalizedConnection())
            using (DbTransaction trans = conn.BeginTransaction())
            {
                for (int i = 0; i < repeats; i++)
                {
                    using (DbCommand cmd = CreateDimensionInsertCommand(conn, trans, new TextDimensionsInsertCommandBuilder()))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            
        }

        [TestMethod]
        public void StoredProcedureDimensionsInsertCommandBuilder_Success()
        {
            using (DbConnection conn = CreateNormalizedConnection())
            using (DbTransaction trans = conn.BeginTransaction())
            {
                for (int i = 0; i < repeats; i++)
                {
                    using (DbCommand cmd = CreateDimensionInsertCommand(conn, trans, new StoredProcedureDimensionsInsertCommandBuilder()))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                trans.Commit();
            }
        }

        [TestMethod]
        public void TextFieldDecimalInsertCommandBuilder_Success()
        {
            Foo(new TextFieldDecimalInsertCommandBuilder());
        }

        [TestMethod]
        public void StoredProcedureFieldDecimalInsertCommandBuilder_Success()
        {
            Foo(new StoredProcedureFieldDecimalInsertCommandBuilder());
        }

        public void Foo(IFieldDecimalInsertCommandBuilder innerCommandBuilder)
        {
            Guid id = Guid.NewGuid();
            Guid revision = Guid.NewGuid();
            IDimensions data = BuildDimensions();
            FieldDecimalInsertCommandBuilder commandBuilder = new FieldDecimalInsertCommandBuilder();

            using (DbConnection conn = CreateDenormalizedConnection())
            {
                using (DbCommand cmd = commandBuilder.Build(conn, innerCommandBuilder, id, revision, "length", 0, data.Length))
                {
                    cmd.ExecuteNonQuery();
                }
                using (DbCommand cmd = commandBuilder.Build(conn, innerCommandBuilder, id, revision, "width", 0, data.Width))
                {
                    cmd.ExecuteNonQuery();
                }
                using (DbCommand cmd = commandBuilder.Build(conn, innerCommandBuilder, id, revision, "height", 0, data.Height))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private DbCommand CreateDimensionInsertCommand(DbConnection conn, DbTransaction trans, IDimensionsInsertCommandBuilder innerCommandBuilder)
        {
            Guid id = Guid.NewGuid();
            Guid revision = Guid.NewGuid();

            DimensionsInsertCommandBuilder commandBuilder = new DimensionsInsertCommandBuilder(innerCommandBuilder);
            DbCommand cmd = commandBuilder.Build(conn, trans, id, revision, BuildDimensions());

            return cmd;
        }

        public IDimensions BuildDimensions()
        {
            Mock<IDimensions> dimensions = new Mock<IDimensions>();
            dimensions.Setup(x => x.Length).Returns(3.0m);
            dimensions.Setup(x => x.Width).Returns(2.0m);
            dimensions.Setup(x => x.Height).Returns(1.0m);

            return dimensions.Object;
        }
        
        private DbConnection CreateNormalizedConnection()
        {
            WidgetsNormalizedConnectionBuilder connectionBuilder = new WidgetsNormalizedConnectionBuilder();

            return connectionBuilder.Build();
        }

        private DbConnection CreateDenormalizedConnection()
        {
            WidgetsDenormalizezdConnectionBuilder connectionBuilder = new WidgetsDenormalizezdConnectionBuilder();

            return connectionBuilder.Build();
        }

        public class WidgetsDenormalizezdConnectionBuilder : ConfigurationConnectionBuilder
        {
            public WidgetsDenormalizezdConnectionBuilder()
                : base("WidgetsDenormalized")
            {
            }
        }

        public class WidgetsNormalizedConnectionBuilder : ConfigurationConnectionBuilder
        {
            public WidgetsNormalizedConnectionBuilder()
                : base("WidgetsNormalized")
            {
            }
        }

        
    }
}
