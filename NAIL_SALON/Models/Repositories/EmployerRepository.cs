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
    internal class EmployerRepository : IRepository<Employer>
    {
        private static EmployerRepository _instance = null;
        public static EmployerRepository Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new EmployerRepository();
                }
                return _instance;
            }
        }
        public void Create(Employer entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = new tbl_Employer
                {
                    name = entity.Name,
                    phone = entity.Phone,
                    password = entity.Password,
                    email = entity.Email,
                    salt = entity.Salt
                };
                en.tbl_Employer.Add(item);
                en.SaveChanges();
                entity.ID = item.id;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public bool Delete(Employer entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Employer.Where(d=>d.id == entity.ID).FirstOrDefault();
                if(item != null)
                {
                    en.tbl_Employer.Remove(item);
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

        public HashSet<Employer> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<Employer> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Employer FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<Employer> GetAll()
        {
            throw new NotImplementedException();
        }

        public HashSet<Employer> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool Update(Employer entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Employer.Where(d=> d.id == entity.ID).FirstOrDefault();
                if (item != null)
                {
                    item.name = entity.Name;
                    item.phone = entity.Phone;
                    item.email = entity.Email;
                    item.password = entity.Password;
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
