using Gnosis.Data.Hydrators;
using System;
using System.Data;

namespace Widgets.Data.Hydrators
{
    public class DatesHydrator : GenericHydrator<IDates>
    {
        public override void Hydrate(IDataReader dr, IDates data)
        {
            if (dr["releaseDate"] != DBNull.Value)
            {
                data.ReleaseDate = (DateTime)dr["releaseDate"];
            }
        }

        protected override void DoHydrate(string key, string value, IDates output)
        {
            switch (key.ToUpper())
            {
                case "RELEASEDATE":
                    output.ReleaseDate = DateTime.Parse(value);
                    break;
            }
        }
    }
}
