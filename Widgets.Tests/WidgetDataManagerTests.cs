using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Gnosis.Extensions;
using Gnosis.Helpers;
using Gnosis.Entities.Data;

using Widgets.Data;

namespace Widgets.Tests
{
    [TestClass]
    public abstract class WidgetDataManagerTests
    {
        private WidgetManager widgetManager;

        protected abstract IWidgetDataManager BuildWidgetDataManager();
        protected abstract IEntityDataManager BuildEntityDataManager();
        protected abstract void ClearPreviousData();

        private Random random = new Random();

        [TestInitialize]
        public void Initialize()
        {
            ClearPreviousData();
        }

        protected decimal GetRandomDecimal()
        {
            return Math.Abs(RandomHelper.Instance.NextDecimal());
        }

        [TestMethod]
        public void CreateUpdate_Success()
        {
            widgetManager = BuildWidgetManager();

            IWidgetCreateRequest createRequest = BuildWidgetCreateRequest();
            Guid createRevision = DoCreate(createRequest);
            Guid id = createRequest.Id;

            Widget widget = widgetManager.RetrieveWidget(id);
            Assert.AreEqual(id, widget.Id);
            Assert.IsTrue(createRequest.Volume.IsVirtuallyEqual(widget.Volume));
            Assert.IsTrue(createRequest.Weight.IsVirtuallyEqual(widget.Weight));
            Assert.IsTrue(createRequest.Width.IsVirtuallyEqual(widget.Width));
            Assert.IsTrue(createRequest.Height.IsVirtuallyEqual(widget.Height));
            Assert.IsTrue(createRequest.Length.IsVirtuallyEqual(widget.Length));
            Assert.AreEqual(createRequest.ReleaseDate, widget.ReleaseDate);

            Mock<IWidgetUpdateRequest> updateRequestMock = new Mock<IWidgetUpdateRequest>();
            updateRequestMock.Setup(x => x.Id).Returns(id);
            updateRequestMock.Setup(x => x.Revision).Returns(createRevision);
            updateRequestMock.Setup(x => x.Updated).Returns(DateTime.UtcNow);

            updateRequestMock.Setup(x => x.Volume).Returns(GetRandomDecimal());
            updateRequestMock.Setup(x => x.Weight).Returns(GetRandomDecimal());

            updateRequestMock.Setup(x => x.Length).Returns(GetRandomDecimal());
            updateRequestMock.Setup(x => x.Height).Returns(GetRandomDecimal());
            updateRequestMock.Setup(x => x.Width).Returns(GetRandomDecimal());

            updateRequestMock.Setup(x => x.ReleaseDate).Returns(DateTime.Today.AddDays(30));

            IWidgetUpdateRequest updateRequest = updateRequestMock.Object;
            Guid updateRevision = widgetManager.UpdateWidget(updateRequest);

            widget = widgetManager.RetrieveWidget(id);
            Assert.AreEqual(id, widget.Id);
            Assert.IsTrue(updateRequest.Volume.IsVirtuallyEqual(widget.Volume));
            Assert.IsTrue(updateRequest.Weight.IsVirtuallyEqual(widget.Weight));
            Assert.IsTrue(updateRequest.Width.IsVirtuallyEqual(widget.Width));
            Assert.IsTrue(updateRequest.Height.IsVirtuallyEqual(widget.Height));
            Assert.IsTrue(updateRequest.Length.IsVirtuallyEqual(widget.Length));
            Assert.AreEqual(updateRequest.ReleaseDate, widget.ReleaseDate);
        }

        [TestMethod]
        public void RetrieveAllWidgets_Success()
        {
            widgetManager = BuildWidgetManager();

            int n = 100;
            for (int i = 0; i < n; i++)
            {
                DoCreate(BuildWidgetCreateRequest());
            }

            ICollection<Widget> widgets = widgetManager.RetrieveAllWidgets();
            Assert.AreEqual(n, widgets.Count);

            foreach (Widget widget in widgets)
            {
                Debug.Print("ID: {0}", widget.Id);
                Debug.Print("Length: {0}", widget.Length);
                Debug.Print("Volume: {0}", widget.Volume);
                Debug.Print("--------------------------------------------------");
            }
        }

        private IWidgetCreateRequest BuildWidgetCreateRequest()
        {
            Guid id = Guid.NewGuid();
            Debug.Print("ID: {0}", id);
            Mock<IWidgetCreateRequest> createRequestMock = new Mock<IWidgetCreateRequest>();
            createRequestMock.Setup(x => x.Id).Returns(id);
            createRequestMock.Setup(x => x.Created).Returns(DateTime.UtcNow);

            createRequestMock.Setup(x => x.Volume).Returns(GetRandomDecimal());
            createRequestMock.Setup(x => x.Weight).Returns(GetRandomDecimal());

            createRequestMock.Setup(x => x.Length).Returns(GetRandomDecimal());
            createRequestMock.Setup(x => x.Height).Returns(GetRandomDecimal());
            createRequestMock.Setup(x => x.Width).Returns(GetRandomDecimal());

            createRequestMock.Setup(x => x.ReleaseDate).Returns(DateTime.Today);

            IWidgetCreateRequest createRequest = createRequestMock.Object;

            return createRequest;
        }

        private Guid DoCreate(IWidgetCreateRequest createRequest)
        {
            Guid createRevision = widgetManager.CreateWidget(createRequest);
            Debug.Print("Create Revision: {0}", createRevision);
            Assert.AreNotEqual(Guid.Empty, createRevision);

            return createRevision;
        }

        private WidgetManager BuildWidgetManager()
        {
            IEntityDataManager entityDataManager = BuildEntityDataManager();
            IWidgetDataManager widgetDataManager = BuildWidgetDataManager();
            return new WidgetManager(entityDataManager, widgetDataManager);
        }
    }
}
