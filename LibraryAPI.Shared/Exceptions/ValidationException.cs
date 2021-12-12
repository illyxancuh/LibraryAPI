using System;

namespace LibraryAPI.BusinessLogic.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
            :base(message)
        {
        }
    }
}
