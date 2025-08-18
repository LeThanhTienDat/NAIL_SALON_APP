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
    internal class AdminRepository : IRepository<Admin>
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
        public void Create(Admin entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = new tbl_Admin
                {
                    name = entity.Name,
                    phone = entity.Phone,
                    password = entity.Password,
                    salt = entity.Salt
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

        public bool Delete(Admin entity)
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

        public HashSet<Admin> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<Admin> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Admin FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<Admin> GetAll()
        {
            throw new NotImplementedException();
        }

        public HashSet<Admin> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool Update(Admin entity)
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
