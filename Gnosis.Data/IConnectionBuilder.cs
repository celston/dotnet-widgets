using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Data
{
    public interface IConnectionBuilder
    {
        DbConnection Build();
    }
}
