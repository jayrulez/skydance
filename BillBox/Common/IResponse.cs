using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillBox.Common
{
    public interface IResponse<T>
    {
        ErrorCode Error { get; set; }        
        T Result { get; set; }
        IList<T> Results { get; set; }
        Boolean IsSuccessful { get; }
        String Comments { get; set; }
    }
}
