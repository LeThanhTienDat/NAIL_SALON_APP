using NAIL_SALON.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models.Repositories
{
    internal class AdminRepository : IRepository<AdminModel>
    {
        private static AdminRepository _instance = null;
        public static AdminRepository Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new AdminRepository();
                }
                return _instance;
            }
        }
        public void Create(AdminModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = new tbl_Admin
                {
                    name = entity.Name,
                    phone = entity.Phone,
                    password = entity.Password,
                    salt = entity.Salt,
                    active = entity.Active                   
                };
                en.tbl_Admin.Add(item);
                en.SaveChanges();
                entity.ID = item.id;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }

        public bool Delete(AdminModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Admin.Where(d => d.id == entity.ID).FirstOrDefault();
                if (item != null)
                {
                    en.tbl_Admin.Remove(item);
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

        public HashSet<AdminModel> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<AdminModel> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public AdminModel FindById(int id)
        {
            throw new NotImplementedException();
        }
        public AdminModel FindByPhone(string phone)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = (from ad in en.tbl_Admin
                            where ad.phone.Equals(phone)
                            select new AdminModel
                            {
                                ID = ad.id,
                                Name = ad.name,
                                Phone = ad.phone,
                                Password = ad.password,
                                Active = ad.active ?? 0,
                                Salt = ad.salt,
                            }).FirstOrDefault();
                return item;    
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public HashSet<AdminModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public HashSet<AdminModel> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool Update(AdminModel entity)
        {

            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Admin.Where(d => d.id == entity.ID).FirstOrDefault();
                if(item != null)
                {
                    item.name = entity.Name;
                    item.password = entity.Password;
                    item.phone = entity.Phone;
                    item.active = entity.Active;
                    item.salt = entity.Salt;
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
