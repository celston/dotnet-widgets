using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class Person : IPerson
    {
        public DateTime Birthdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age
        {
            get
            {
                TimeSpan ts = DateTime.Today - Birthdate;
                return (int)Math.Floor(ts.TotalDays / 365);
            }
        }
    }
}
