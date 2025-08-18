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
    internal class ServiceProductRepository : IRepository<ServiceProduct>
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
        public void Create(ServiceProduct entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(ServiceProduct entity)
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

        public HashSet<ServiceProduct> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<ServiceProduct> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public ServiceProduct FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<ServiceProduct> GetAll()
        {
            throw new NotImplementedException();
        }

        public HashSet<ServiceProduct> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool Update(ServiceProduct entity)
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
