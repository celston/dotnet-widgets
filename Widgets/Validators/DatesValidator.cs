using Gnosis;
using System;
using Widgets.Exceptions;

namespace Widgets.Validators
{
    public class DatesValidator
    {
        public static DateTime MinReleaseDate = new DateTime(1980, 01, 01);
        public static DateTime MaxReleaseDate = new DateTime(2030, 01, 01);

        public void Validate(IDates data)
        {
            if (data.ReleaseDate.HasValue && (data.ReleaseDate < MinReleaseDate || data.ReleaseDate > MaxReleaseDate))
            {
                throw new InvalidReleaseDateException(data.ReleaseDate);
            }
        }
    }
}
