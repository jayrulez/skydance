using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Billbox.Common
{
    public class Response<T>
    {
        public ErrorCode Error { get; set; }
        public String ErrorMessage { get; set; }
        public T Result { get; set; }
        public IList<T> Results { get; set; }

        public Boolean IsSuccessful 
        { 
            get 
            {
                return Error == ErrorCode.NoError;
            }
        }

        public Response()
        {
            this.Error = ErrorCode.NoError;
        }

        
    }
}