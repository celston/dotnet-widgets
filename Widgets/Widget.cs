using Gnosis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Widgets
{
    public class Widget : Entity, IWidget
    {
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public decimal Volume { get; set; }
        public decimal Weight { get; set; }
        public decimal Width { get; set; }
    }
}
