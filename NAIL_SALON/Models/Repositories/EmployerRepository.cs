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
    internal class EmployerRepository : IRepository<EmployerModel>
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
        public void Create(EmployerModel entity)
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
                    salt = entity.Salt,
                    active = entity.Active,
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

        public bool Delete(EmployerModel entity)
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

        public HashSet<EmployerModel> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<EmployerModel> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public EmployerModel FindById(int id)
        {
            throw new NotImplementedException();
        }
        public EmployerModel FindByPhone(string phone)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = (from em in en.tbl_Employer
                            where em.phone.Equals(phone)
                            select new EmployerModel
                            {
                                ID = em.id,
                                Name = em.name,
                                Phone = em.phone,
                                Password = em.password,
                                Email = em.email,
                                Salt = em.salt,
                                Active = em.active ?? 0,
                            }).FirstOrDefault();
                return item;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public HashSet<EmployerModel> GetAll()
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = (from em in en.tbl_Employer
                            orderby em.id descending
                            select new EmployerModel
                            {
                                ID = em.id,
                                Name = em.name,
                                Phone = em.phone,
                                Password = em.password,
                                Email = em.email,
                                Salt = em.salt,
                                Active = em.active ?? 0
                            }).ToHashSet();
                return item;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new HashSet<EmployerModel>();
        }

        public HashSet<EmployerModel> GetAllPaging(int index = 1, int pageSize = 10)
        {
            var allEmployers = this.GetAll();
            if (index < 1) index = 1;

            return allEmployers
                .Skip((index - 1) * pageSize)
                .Take(pageSize)
                .ToHashSet();
        }

        public bool Update(EmployerModel entity)
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

        public bool ChangeActive(EmployerModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Employer.Where(d=>d.id == entity.ID).FirstOrDefault();
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
