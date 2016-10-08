using Gnosis.Data.Hydrators;
using System;
using System.Data;

namespace Fruits
{
    public class FruitHydrator : GenericHydrator<IFruit>
    {
        public override void Hydrate(IDataReader dr, IFruit data)
        {
            throw new NotImplementedException();
        }

        protected override void DoHydrate(string key, string value, IFruit output)
        {
            switch (key.ToUpper())
            {
                case "WEIGHT":
                    output.Weight = decimal.Parse(value);
                    break;
            }
        }
    }
}
