using System.Data;
using Gnosis.Data.Hydrators;

namespace Widgets.Data.Hydrators
{
    public class SpecificationsHydrator : GenericHydrator<ISpecifications>
    {
        public override void Hydrate(IDataReader dr, ISpecifications data)
        {
            data.Weight = GetDecimal(dr, "weight");
            data.Volume = GetDecimal(dr, "volume");
        }

        protected override void DoHydrate(string key, string value, ISpecifications output)
        {
            switch (key.ToUpper())
            {
                case "WEIGHT":
                    output.Weight = decimal.Parse(value);
                    break;

                case "VOLUME":
                    output.Volume = decimal.Parse(value);
                    break;
            }
        }
    }
}
