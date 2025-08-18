using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAIL_SALON.Models.Entities;
using System.Data.Entity.Core;
using System.Diagnostics;

namespace NAIL_SALON.Models.Repositories
{
    internal class CategoryRepository : IRepository<Category>
    {
        private static CategoryRepository _instance = null;
        public static CategoryRepository Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new CategoryRepository();
                }
                return _instance;
            }
        }
        public void Create(Category entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = new tbl_Category
                {
                    name = entity.Name,
                    active = entity.Active
                };
                en.tbl_Category.Add(item);
                en.SaveChanges();
                entity.ID = item.id;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        public bool Delete(Category entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Category.Where(d => d.id == entity.ID).FirstOrDefault();
                if (item != null)
                {
                    en.tbl_Category.Remove(item);
                    en.SaveChanges();
                    return true;
                }
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public HashSet<Category> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<Category> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Category FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<Category> GetAll()
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var items = (from cate in en.tbl_Category
                             select new Category
                             {
                                 ID = cate.id,
                                 Name = cate.name,
                                 Active = cate.active ?? 0
                             }).ToHashSet();
                return items;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new HashSet<Category>();
        }

        public HashSet<Category> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool Update(Category entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Category.Where(d=>d.id == entity.ID).FirstOrDefault();
                if(item != null)
                {
                    item.name = entity.Name;
                    item.active = entity.Active;
                    en.SaveChanges();
                    return true;
                }
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }
    }
}
