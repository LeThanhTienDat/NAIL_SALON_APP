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
    internal class ServiceProductRepository : IRepository<ServiceProductModel>
    {
        private static ServiceProductRepository _instance = null;
        public static ServiceProductRepository Instance
        {
            get
            {
                if( _instance == null)
                {
                    _instance = new ServiceProductRepository();
                }
                return _instance;   
            }
        }       
        public void Create(ServiceProductModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = new tbl_ServiceProduct
                {
                    service_id = entity.ServiceId,
                    product_id = entity.ProductId,
                    quantity = entity.Quantity,
                };
                en.tbl_ServiceProduct.Add(item);
                en.SaveChanges();
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }      
        }
        

        public bool Delete(ServiceProductModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_ServiceProduct.Where(d=> d.product_id == entity.ProductId && d.service_id == entity.ServiceId).FirstOrDefault();
                if (item != null)
                {
                    en.tbl_ServiceProduct.Remove(item);
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
        public bool DeleteById(int serviceId)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var items = en.tbl_ServiceProduct.Where(d => d.service_id == serviceId).ToList();
                foreach(var item in items)
                {
                    en.tbl_ServiceProduct.Remove(item);
                    en.SaveChanges();
                }
                return true;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public HashSet<ServiceProductModel> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<ServiceProductModel> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public ServiceProductModel FindById(int id)
        {
            throw new NotImplementedException();
        }
        public HashSet<ServiceProductModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public HashSet<ServiceProductModel> GetAllByServiceId(int Id)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var items = (from serPro in en.tbl_ServiceProduct
                             join pro in en.tbl_Product on serPro.product_id equals pro.id into proGroup
                             from pr in proGroup.DefaultIfEmpty()
                             join cate in en.tbl_Category on pr != null ? pr.category_id : 0 equals cate.id into cateGroup
                             from ca in cateGroup.DefaultIfEmpty()
                             where serPro.service_id == Id
                             select new ServiceProductModel
                             {
                                 ServiceId = serPro.service_id,
                                 ProductId = serPro.product_id,
                                 Quantity = serPro.quantity ?? 0,
                                 CurrentProductBelong = pr == null ? null : new ProductModel
                                 {
                                     ID = pr.id,
                                     Name = pr.name,
                                     Description = pr.description,
                                     Price = pr.price ?? 0,
                                     CategoryId = pr.category_id,
                                     Stock = pr.stock ?? 0,
                                     Active = pr.active ?? 0,
                                     Image = pr.image,
                                     CategoryName = ca != null ? ca.name : null
                                 }
                             }).ToHashSet();
                return items;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new HashSet<ServiceProductModel>();
        }

        public HashSet<ServiceProductModel> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool Update(ServiceProductModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_ServiceProduct.Where(d=>d.service_id==entity.ServiceId && d.product_id == entity.ProductId).FirstOrDefault();
                if (item != null)
                {
                    item.service_id = entity.ServiceId;
                    item.product_id = entity.ProductId;
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
