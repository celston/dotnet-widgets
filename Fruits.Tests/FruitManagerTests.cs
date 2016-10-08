using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Fruits.Oranges;
using Fruits.Bananas;
using Gnosis.Extensions;
using Gnosis.Data;
using Gnosis.Entities.Data.CommandBuilders.FieldVarchar;
using Gnosis.Entities.Data.CommandBuilders.Entity;
using Gnosis.Entities.Data.CommandBuilders.EntityRevision;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using Gnosis.Entities.Data.CommandBuilders.FieldVarcharActive;
using Gnosis.Data.Commands;

namespace Fruits.Tests
{
    [TestClass]
    public class FruitManagerTests
    {
        public FruitManager fruitManager = new FruitManager();

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
        public void CreateRetrieveUpdate_ValenciaOrange_Success()
        {
            IValenciaOrangeCreateRequest createRequest = BuildFruitCreateRequest<IValenciaOrangeCreateRequest>();
            Guid id = createRequest.Id;

            Guid createRevision = fruitManager.CreateValenciaOrange(createRequest);

            ValenciaOrange entity = fruitManager.RetrieveValenciaOrange(id);
            AssertCreatedFruit(createRequest, entity);

            IValenciaOrangeUpdateRequest updateRequest = BuildFruitUpdateRequest<IValenciaOrangeUpdateRequest>(id, createRevision);
            Guid updateRevision = fruitManager.UpdateValenciaOrange(updateRequest);

            entity = fruitManager.RetrieveValenciaOrange(createRequest.Id);
            AssertUpdatedFruit(updateRequest, entity);
        }

        [TestMethod]
        public void CreateAndRetrieve_CavendishBanana_Success()
        {
            ICavendishBananaCreateRequest createRequest = BuildFruitCreateRequest<ICavendishBananaCreateRequest>();

            fruitManager.CreateCavendishBanana(createRequest);

            CavendishBanana entity = fruitManager.RetrieveCavendishBanana(createRequest.Id);
            AssertCreatedFruit(createRequest, entity);
        }

        [TestMethod]
        public void RetrieveAllFruit_Success()
        {
            int n = 100;
            for (int i = 0; i < n; i++)
            {
                fruitManager.CreateValenciaOrange(BuildFruitCreateRequest<IValenciaOrangeCreateRequest>());
                fruitManager.CreateCavendishBanana(BuildFruitCreateRequest<ICavendishBananaCreateRequest>());
            }

            ICollection<Fruit> fruit = fruitManager.RetrieveAllFruit();
            Assert.AreEqual(n * 2, fruit.Count);
            Assert.AreEqual(n, fruit.OfType<ValenciaOrange>().Count());
            Assert.AreEqual(n, fruit.OfType<CavendishBanana>().Count());
        }

        private void AssertCreatedFruit(IFruitCreateRequest createRequest, Fruit entity)
        {
            Assert.AreEqual(createRequest.Id, entity.Id);
            Assert.IsTrue(createRequest.Created.VirtuallyEquals(entity.Created));
            Assert.AreEqual(createRequest.Weight, entity.Weight);
        }

        private void AssertUpdatedFruit(IFruitUpdateRequest updateRequest, Fruit entity)
        {
            Assert.AreEqual(updateRequest.Id, entity.Id);
            Assert.IsTrue(updateRequest.Updated.VirtuallyEquals(entity.Updated));
            Assert.AreEqual(updateRequest.Weight, entity.Weight);
        }

        private T BuildFruitCreateRequest<T>()
            where T : class, IFruitCreateRequest
        {
            Mock<T> createRequestMock = new Mock<T>();

            Guid id = Guid.NewGuid();
            createRequestMock.Setup(x => x.Id).Returns(id);
            createRequestMock.Setup(x => x.Created).Returns(DateTime.UtcNow);
            createRequestMock.Setup(x => x.Weight).Returns(Gnosis.Helpers.RandomHelper.Instance.NextDecimal());

            return createRequestMock.Object;
        }

        private T BuildFruitUpdateRequest<T>(Guid id, Guid revision)
            where T : class, IFruitUpdateRequest
        {
            Mock<T> updateRequestMock = new Mock<T>();

            updateRequestMock.Setup(x => x.Id).Returns(id);
            updateRequestMock.Setup(x => x.Revision).Returns(revision);
            updateRequestMock.Setup(x => x.Updated).Returns(DateTime.UtcNow);
            updateRequestMock.Setup(x => x.Weight).Returns(Gnosis.Helpers.RandomHelper.Instance.NextDecimal());

            return updateRequestMock.Object;
        }
    }
}
