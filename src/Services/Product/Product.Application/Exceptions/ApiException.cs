 
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Product.Application.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException() : base() { }

        public ApiException(string message) : base(message) { }

        public ApiException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
