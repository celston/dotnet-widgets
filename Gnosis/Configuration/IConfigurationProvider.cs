using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Configuration
{
    public interface IConfigurationProvider
    {
        string GetString(string key);
        int? GetInt(string key);
        decimal? GetDecimal(string key);
    }
}
