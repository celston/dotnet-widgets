using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public interface IPerson
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime Birthdate { get; set; }
    }
}
