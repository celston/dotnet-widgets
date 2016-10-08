using Gnosis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Exceptions
{
    public class FutureBirthdateException : GException
    {
        public FutureBirthdateException(DateTime birthdate) : base("The birthdate {0} is invalid because it is in the future.", birthdate)
        {
        }
    }

    public class EmptyBirthdateException : GException
    {
    }
}
