using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widgets
{
    public interface IDimensions
    {
        decimal Length { get; set; }
        decimal Height { get; set; }
        decimal Width { get; set; }
    }
}
