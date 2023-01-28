using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybersoft.ApplicationCore.Specifications
{
    public class PaginatedProductsSpecification : BaseSpecification<Products, int>
    {
        public PaginatedProductsSpecification(int pagesize, int pagenumber)
            : base(null)
        {
            if (pagenumber < 1)
            {
                pagenumber = 1;
            }

            if (pagesize > 0)
            {
                ApplyPaging((pagenumber - 1) * pagesize, pagesize);
            }
        }
    }

    public class ProductsTotalCountSpecification : BaseSpecification<Products, int>
    {
        public ProductsTotalCountSpecification()
            : base(null)
        {
            ApplyOrderByDescending(x => x.AddedDate);
        }
    }

    public class ProductByDeletedStatusSpecification : BaseSpecification<Products, int>
    {
        public ProductByDeletedStatusSpecification()
            : base(x => x.IsDeleted == false)
        {

        }
    }

    public class ProductByProductIdSpecification : BaseSpecification<Products, int>
    {
        public ProductByProductIdSpecification(int productId)
            : base(x => x.Id == productId)
        {

        }
    }
    public class ProductByProductNameSpecification : BaseSpecification<Products, int>
    {
        public ProductByProductNameSpecification(string productName)
            : base(x => x.Name == productName)
        {

        }
    }


}
