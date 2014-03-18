using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BillBox.Models
{
    public class PagerModel<T>
    {
        public IEnumerable<T> model;
    }
}