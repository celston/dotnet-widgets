using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Gnosis.Entities.Data;
using Gnosis.Entities.Validators;

namespace Gnosis.Entities.Tests.Validators
{
    [TestClass]
    public class EntityCreateValidatorTests
    {
        private EntityCreateValidator validator = new EntityCreateValidator();

        [TestMethod]
        [ExpectedException(typeof(EntityIdEmptyException))]
        public void Validate_EmptyId_Fail()
        {
            Mock<IEntityDataManager> entityDataManager = new Mock<IEntityDataManager>();
            Mock<IEntityCreateRequest> request = new Mock<IEntityCreateRequest>();

            validator.Validate(entityDataManager.Object, request.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityCreatedEmptyException))]
        public void Validate_EmptyCreated_Fail()
        {
            Mock<IEntityDataManager> entityDataManager = new Mock<IEntityDataManager>();
            Mock<IEntityCreateRequest> request = new Mock<IEntityCreateRequest>();

            request.Setup(x => x.Id).Returns(Guid.NewGuid());

            validator.Validate(entityDataManager.Object, request.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(EntityExistsException))]
        public void Validate_EntityExists_Fail()
        {
            Mock<IEntityDataManager> entityDataManager = new Mock<IEntityDataManager>();
            Mock<IEntityCreateRequest> request = new Mock<IEntityCreateRequest>();

            entityDataManager.Setup(x => x.GetEntityCountById(It.IsAny<Guid>())).Returns(1);

            request.Setup(x => x.Id).Returns(Guid.NewGuid());
            request.Setup(x => x.Created).Returns(DateTime.UtcNow);

            validator.Validate(entityDataManager.Object, request.Object);
        }

        [TestMethod]
        public void Validate_Valid_Success()
        {
            Mock<IEntityDataManager> entityDataManager = new Mock<IEntityDataManager>();
            Mock<IEntityCreateRequest> request = new Mock<IEntityCreateRequest>();

            request.Setup(x => x.Id).Returns(Guid.NewGuid());
            request.Setup(x => x.Created).Returns(DateTime.UtcNow);

            validator.Validate(entityDataManager.Object, request.Object);
        }
    }
}
