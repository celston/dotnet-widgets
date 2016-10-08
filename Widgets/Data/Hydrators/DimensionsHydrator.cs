using Gnosis.Data.Hydrators;
using System.Data;

namespace Widgets.Data.Hydrators
{
    public class DimensionsHydrator : GenericHydrator<IDimensions>
    {
        public override void Hydrate(IDataReader dr, IDimensions data)
        {
            data.Height = GetDecimal(dr, "height");
            data.Length = GetDecimal(dr, "length");
            data.Width = GetDecimal(dr, "width");
        }

        protected override void DoHydrate(string key, string value, IDimensions output)
        {
            DecimalPropertyHydrator decimalPropertyHydrator = new DecimalPropertyHydrator();

            switch (key)
            {
                case "Length":
                case "Height":
                case "Width":
                    decimalPropertyHydrator.Hydrate(key, value, output);
                    break;
            }
        }
    }
}
