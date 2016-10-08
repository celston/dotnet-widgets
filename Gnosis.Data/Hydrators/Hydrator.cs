using System;
using System.Data;

namespace Gnosis.Data.Hydrators
{
    public abstract class Hydrator
    {
        protected Guid GetGuid(IDataReader dr, string name)
        {
            return dr.GetGuid(dr.GetOrdinal(name));
        }

        protected DateTime GetDateTime(IDataReader dr, string name)
        {
            return dr.GetDateTime(dr.GetOrdinal(name));
        }

        protected decimal GetDecimal(IDataReader dr, string name)
        {
            return dr.GetDecimal(dr.GetOrdinal(name));
        }
    }
}
