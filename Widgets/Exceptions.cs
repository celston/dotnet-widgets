using Gnosis;
using System;

namespace Widgets.Exceptions
{
    public class InvalidReleaseDateException : GException
    {
        public InvalidReleaseDateException(DateTime? ReleaseDate)
            : base("Release date {0} invalid, must be between {1} {2}", ReleaseDate, Validators.DatesValidator.MinReleaseDate, Validators.DatesValidator.MaxReleaseDate)
        {
        }
    }
}