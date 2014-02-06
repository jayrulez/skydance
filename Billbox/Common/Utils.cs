using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billbox.Common
{
    public enum ErrorCode
    {
        NoError, UserNotFound, DuplicateEmailAddress, DuplicateUsername, NoResultsFound,
        FKError,
        DbError, DBEntityValidationError, SysError, Generic1, Generic2

    }
}