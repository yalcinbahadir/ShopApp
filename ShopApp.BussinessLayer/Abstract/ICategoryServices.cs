using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.BussinessLayer.Abstract
{
    public interface ICategoryServices:IRepository<Category>
    {
        //Comes from IRepository
        //Category GetById(int id);
        //List<Category> GetAll();
        //void Create(Category entity);
        //bool Update(Category entity);
        //bool Delete(Category entity);

        //Comes from ICategoryRepository
        public void DoSthForCategory();
        //A special Method for Bussinesslayer of category
        public void DoSthSpecialForCategory();
    }
}
