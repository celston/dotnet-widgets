using Gnosis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widgets.Data
{
    public interface IWidgetDataManager
    {
        void CreateWidget(Guid revision, IWidgetCreateRequest request);
        void UpdateWidget(Guid revision, IWidgetUpdateRequest request);
        void HydrateWidget(Guid id, Widget widget);
        ICollection<Widget> RetrieveAllWidgets();
    }
}
