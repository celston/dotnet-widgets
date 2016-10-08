using System;
using System.Collections.Generic;

using Gnosis.Entities.Data;
using Gnosis.Entities.Validators;

using Widgets.Data;
using Widgets.Validators;

namespace Widgets
{
    public class WidgetManager
    {
        private IEntityDataManager entityDataManager;
        private IWidgetDataManager widgetDataManager;

        private EntityCreateValidator entityCreateValidator = new EntityCreateValidator();
        private EntityUpdateValidator entityUpdateValidator = new EntityUpdateValidator();
        private WidgetValidator widgetValidator = new WidgetValidator();

        public WidgetManager(IEntityDataManager entityDataManager, IWidgetDataManager widgetDataManager)
        {
            this.entityDataManager = entityDataManager;
            this.widgetDataManager = widgetDataManager;
        }

        public Guid CreateWidget(IWidgetCreateRequest request)
        {
            Guid revision = Guid.NewGuid();

            entityCreateValidator.Validate(entityDataManager, request);
            widgetValidator.Validate(request);

            widgetDataManager.CreateWidget(revision, request);

            return revision;
        }

        public Guid UpdateWidget(IWidgetUpdateRequest request)
        {
            Guid revision = Guid.NewGuid();

            entityUpdateValidator.Validate(entityDataManager, request);
            widgetValidator.Validate(request);

            widgetDataManager.UpdateWidget(revision, request);

            return revision;
        }

        public Widget RetrieveWidget(Guid id)
        {
            Widget widget = new Widget();

            widgetDataManager.HydrateWidget(id, widget);

            return widget;
        }

        public ICollection<Widget> RetrieveAllWidgets()
        {
            return widgetDataManager.RetrieveAllWidgets();
        }
    }
}
