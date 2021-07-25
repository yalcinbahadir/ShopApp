using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.DataAccess.Concrete
{
    public class EFCORECategoryRepository : EFCOREGenericRepository<Category, ShopContext>, ICategoryRepository
    {
        public void DoSthForCategory()
        {
            throw new NotImplementedException();
        }
    }
}
