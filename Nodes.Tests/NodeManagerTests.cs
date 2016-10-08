using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Gnosis.Data;
using Gnosis.Entities.Data.CommandBuilders.FieldVarchar;
using Gnosis.Entities.Data.CommandBuilders.Entity;
using Gnosis.Entities.Data.CommandBuilders.EntityRevision;
using System.Data.Common;
using Gnosis.Entities.Data.CommandBuilders.FieldUniqueIdentifier;
using System.Collections.Generic;
using Gnosis.Data.Commands;
using Gnosis.Entities.Data.CommandBuilders.FieldDecimal;

namespace Nodes.Tests
{
    [TestClass]
    public class NodeManagerTests
    {
        private NodeManager manager = new NodeManager();

        [TestMethod]
        public void CreateNode_NoParent_Success()
        {
            Guid id = DoCreateParentNode();

            Node node = manager.RetrieveNode(id);
            Assert.AreEqual(id, node.Id);
            Assert.IsFalse(node.Parent.HasValue);
            Assert.AreEqual(0, node.Children.Count);
        }

        [TestMethod]
        public void CreateNode_ParentAndChildren_Success()
        {
            Guid parent = DoCreateParentNode();

            int n = 3;
            List<Guid> children = new List<Guid>();
            for (int i = 0; i < n; i++)
            {
                Guid id = DoCreateChildNode(parent);
                for (int j = 0; j < n; j++)
                {
                    DoCreateChildNode(id);
                }
                children.Add(id);
            }

            Node node = manager.RetrieveNode(parent);
            Assert.AreEqual(parent, node.Id);
            Assert.IsFalse(node.Parent.HasValue);
            Assert.AreEqual(n, node.Children.Count);
            foreach (Node child in node.Children)
            {
                Assert.AreEqual(node.Id, child.Parent);
                Assert.AreEqual(3, child.Children.Count);
                foreach (Node grandChild in child.Children)
                {
                    Assert.AreEqual(child.Id, grandChild.Parent);
                    Assert.AreEqual(0, grandChild.Children.Count);
                }
            }
        }

        private Guid DoCreateChildNode(Guid parent)
        {
            Mock<INodeCreateRequest> createRequestMock = new Mock<INodeCreateRequest>();

            Guid id = Guid.NewGuid();
            createRequestMock.Setup(x => x.Id).Returns(id);
            createRequestMock.Setup(x => x.Created).Returns(DateTime.UtcNow);
            createRequestMock.Setup(x => x.Parent).Returns(parent);

            Guid revision = manager.CreateNode(createRequestMock.Object);

            return id;
        }

        private Guid DoCreateParentNode()
        {
            Mock<INodeCreateRequest> createRequestMock = new Mock<INodeCreateRequest>();

            Guid id = Guid.NewGuid();
            createRequestMock.Setup(x => x.Id).Returns(id);
            createRequestMock.Setup(x => x.Created).Returns(DateTime.UtcNow);

            Guid revision = manager.CreateNode(createRequestMock.Object);

            return id;
        }

        [TestInitialize]
        public void Initialize()
        {
            ConfigurationConnectionBuilder connectionBuilder = new ConfigurationConnectionBuilder("WidgetsDenormalized");

            List<TruncateTableCommandBuilder> list = new List<TruncateTableCommandBuilder>()
            {
                new FieldVarcharTruncateCommandBuilder(),
                new FieldUniqueIdentifierTruncateCommandBuilder(),
                new FieldDecimalTruncateCommandBuilder(),
                new EntityTruncateCommandBuilder(),
                new EntityRevisionTruncateCommandBuilder()
            };

            using (DbConnection conn = connectionBuilder.Build())
            {
                foreach (TruncateTableCommandBuilder cb in list)
                {
                    using (DbCommand cmd = cb.Build(conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
