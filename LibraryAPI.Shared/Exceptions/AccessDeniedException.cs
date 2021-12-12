using System;

namespace LibraryAPI.BusinessLogic.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException()
            :base("User has no access to perform this action.")
        {
        }
    }
}
