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
    internal class ProductRepository : IRepository<Product>
    {
        private static ProductRepository _instance = null;
        public static ProductRepository Instance
        {
            get
            {
                if( _instance == null)
                {
                    _instance = new ProductRepository();
                }
                return _instance;   
            }
        }
        public void Create(Product entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = new tbl_Product
                {
                    name = entity.Name,
                    description = entity.Description,
                    price = entity.Price,
                    category_id = entity.CategoryId,
                    stock = entity.Stock,
                    active = entity.Active,
                    image = entity.Image
                };
                en.tbl_Product.Add(item);
                en.SaveChanges();
                entity.ID = item.id;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }            
        }

        public bool Delete(Product entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Product.Where(d=>d.id == entity.ID).FirstOrDefault();
                if (item != null)
                {
                    en.tbl_Product.Remove(item);
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

        public HashSet<Product> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<Product> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Product FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public HashSet<Product> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Product.Where(d=>d.id==entity.ID).FirstOrDefault();
                if (item != null)
                {
                    en.tbl_Product.Remove(item);
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
