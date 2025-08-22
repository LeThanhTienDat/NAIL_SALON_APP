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
    internal class ProductRepository : IRepository<ProductModel>
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
        public void Create(ProductModel entity)
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

        public bool Delete(ProductModel entity)
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

        public HashSet<ProductModel> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<ProductModel> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public ProductModel FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<ProductModel> GetAll()
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var items = (from pro in en.tbl_Product
                             join cate in en.tbl_Category on pro.category_id equals cate.id
                             orderby pro.id descending
                             select new ProductModel
                             {
                                 ID = pro.id,
                                 Name = pro.name,
                                 Description = pro.description,
                                 Price = pro.price ?? 0,
                                 CategoryId = pro.category_id,
                                 Stock = pro.stock ?? 0,
                                 Active = pro.active ?? 0,
                                 Image = pro.image,
                                 CategoryName = cate.name
                             }).ToHashSet();
                return items;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new HashSet<ProductModel>();
        }

        public HashSet<ProductModel> GetAllPaging(int index = 1, int pageSize = 10)
        {
            try
            {
                var allProducts = this.GetAll();
                if (index < 1) index = 1;
                return allProducts
                        .Skip((index - 1) * pageSize)
                        .Take(pageSize)
                        .ToHashSet();
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new HashSet<ProductModel>();
        }

        public bool Update(ProductModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Product.Where(d=>d.id==entity.ID).FirstOrDefault();
                if (item != null)
                {
                    item.name = entity.Name;
                    item.price = entity.Price;
                    item.description = entity.Description;
                    item.stock = entity.Stock;
                    item.active = entity.Active;
                    item.category_id = entity.CategoryId;
                    item.image = entity.Image;
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
        public bool ChangeActive(ProductModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Product.Where(d=>d.id == entity.ID).FirstOrDefault();
                if (item != null)
                {
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
