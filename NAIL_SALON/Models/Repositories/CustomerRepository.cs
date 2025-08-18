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
    internal class CustomerRepository : IRepository<Customer>
    {
        private static CustomerRepository _instance = null;
        public static CustomerRepository Instance
        {
            get
            {
                if( _instance == null)
                {
                    _instance = new CustomerRepository();
                }
                return _instance;
            }
        }
        public void Create(Customer entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = new tbl_Customer
                {
                    name = entity.Name,
                    phone = entity.Phone,
                    address = entity.Address,
                    district_id = entity.DistrictId,
                    city_id = entity.CityId,
                    birthday = entity.BirthDay
                };
                en.tbl_Customer.Add(item);
                en.SaveChanges();
                entity.ID = item.id;    
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public bool Delete(Customer entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Customer.Where(d=>d.id == entity.ID).FirstOrDefault();
                if (item != null)
                {
                    en.tbl_Customer.Remove(item);
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

        public HashSet<Customer> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<Customer> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Customer FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public HashSet<Customer> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool Update(Customer entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Customer.Where(d=>d.id == entity.ID).FirstOrDefault();
                if(item != null)
                {
                    item.name = entity.Name;
                    item.phone = entity.Phone;
                    item.address = entity.Address;
                    item.district_id = entity.DistrictId;
                    item.city_id = entity.CityId;
                    item.birthday = entity.BirthDay;
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
