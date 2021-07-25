using ShopApp.BussinessLayer.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.BussinessLayer.Concrete
{
    public class CategoryManager : ICategoryServices
    {
        private ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void Create(Category entity)
        {
            _categoryRepository.Create(entity);
        }

        public bool Delete(Category entity)
        {
            return _categoryRepository.Delete(entity);
        }

        public void DoSthForCategory()
        {
            //Implementation of ICategoryRepository specific method
            throw new NotImplementedException();
        }

        public void DoSthSpecialForCategory()
        {
            //Implementation of ICategorySevices specific method 
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
            return _categoryRepository.GetById(id);
        }

        public bool Update(Category entity)
        {
            return _categoryRepository.Update(entity);
        }
    }
}
