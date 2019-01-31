using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YeZiFang.App.Models
{
    public class PagingList<T>
    {
        public long TotalCount;
        public List<T> ReturnData;
    }
}
