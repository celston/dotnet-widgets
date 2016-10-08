using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using Gnosis.Data;
using Gnosis.Data.Commands;
using System.Collections.Generic;
using Gnosis.Entities.Data.CommandBuilders.FieldVarchar;
using Gnosis.Entities.Data.CommandBuilders.FieldVarcharActive;
using Gnosis.Entities.Data.CommandBuilders.Entity;
using Gnosis.Entities.Data.CommandBuilders.EntityRevision;
using System.Data.Common;

namespace People.Tests
{
    [TestClass]
    public class PeopleManagerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            ConfigurationConnectionBuilder connectionBuilder = new ConfigurationConnectionBuilder("WidgetsDenormalized");

            List<TruncateTableCommandBuilder> truncateTableCommandBuilders = new List<TruncateTableCommandBuilder>();
            truncateTableCommandBuilders.Add(new FieldVarcharTruncateCommandBuilder());
            truncateTableCommandBuilders.Add(new FieldVarcharActiveTruncateCommandBuilder());
            truncateTableCommandBuilders.Add(new EntityTruncateCommandBuilder());
            truncateTableCommandBuilders.Add(new EntityRevisionTruncateCommandBuilder());

            using (DbConnection conn = connectionBuilder.Build())
            {
                foreach (TruncateTableCommandBuilder builder in truncateTableCommandBuilders)
                {
                    using (DbCommand cmd = builder.Build(conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        [TestMethod]
        public void Create_Normal_Success()
        {
            Mock<IPersonCreateRequest> createRequestMock = new Mock<IPersonCreateRequest>();

            Guid id = Guid.NewGuid();
            createRequestMock.Setup(x => x.Id).Returns(id);
            createRequestMock.Setup(x => x.Created).Returns(DateTime.UtcNow);
            createRequestMock.Setup(x => x.Birthdate).Returns(new DateTime(1980, 7, 26));

            PeopleManager peopleManager = new PeopleManager();
            Guid createRevision = peopleManager.CreatePerson(createRequestMock.Object);
        }
    }
}
