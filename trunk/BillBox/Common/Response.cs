﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillBox.Common
{
    /// <summary>
    /// Responsible for transporting
    /// </summary>
    /// <typeparam name="T">the generic typename</typeparam>
    public class Response<T> : IResponse<T>
    {
        public ErrorCode Error { get; set; }   
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

        public String Comments { get; set; }
        
    }
}