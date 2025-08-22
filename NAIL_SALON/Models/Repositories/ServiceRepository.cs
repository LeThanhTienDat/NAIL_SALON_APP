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
    internal class ServiceRepository : IRepository<ServiceModel>
    {
        private static ServiceRepository _instance = null;
        public static ServiceRepository Instance
        {
            get
            {
                if( _instance == null)
                {
                    _instance = new ServiceRepository();
                }
                return _instance;
            }
        }
        public void Create(ServiceModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = new tbl_Service
                {
                    name = entity.Name,
                    description = entity.Description,
                    price = entity.Price,
                    active = entity.Active,                    
                    discount = entity.Discount
                };
                en.tbl_Service.Add(item);
                en.SaveChanges();
                entity.ID = item.id;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            } 
        }

        public bool Delete(ServiceModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Service.Where(d=>d.id == entity.ID).FirstOrDefault();
                if (item != null)
                {
                    en.tbl_Service.Remove(item);
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

        public HashSet<ServiceModel> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<ServiceModel> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public ServiceModel FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<ServiceModel> GetAll()
        {
            try
            {

            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new HashSet<ServiceModel>();
        }

        public HashSet<ServiceModel> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool Update(ServiceModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Service.Where(d=>d.id==entity.ID).FirstOrDefault();
                if (item != null)
                {
                    item.name = entity.Name;
                    item.description = entity.Description;
                    item.price = entity.Price;
                    item.active = entity.Active;
                    item.discount = entity.Discount;
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
