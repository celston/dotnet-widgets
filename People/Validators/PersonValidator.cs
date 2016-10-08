using People.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Validators
{
    public class PersonValidator
    {
        public void Validate(IPerson data)
        {
            if (data.Birthdate > DateTime.Now)
            {
                throw new FutureBirthdateException(data.Birthdate);
            }
            if (data.Birthdate == DateTime.MinValue)
            {
                throw new EmptyBirthdateException();
            }
        }
    }
}
