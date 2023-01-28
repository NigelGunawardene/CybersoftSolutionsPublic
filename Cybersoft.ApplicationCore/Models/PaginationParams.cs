using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Models
{
    public class PaginationParams : QueryPaginationParameters
    {
        public PaginationParams(int pagesize, int pagenumber)
        {
            this.PageSize = pagesize;
            this.PageNumber = pagenumber;
        }
    }
}
