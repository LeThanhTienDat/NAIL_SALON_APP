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
    internal class CityRepository : IRepository<CityModel>
    {
        private static CityRepository _instance = null;
        public static CityRepository Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new CityRepository();
                }
                return _instance;
            }
        }
        public void Create(CityModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = new tbl_City
                {
                    city_name = entity.CityName
                };
                en.tbl_City.Add(item);
                en.SaveChanges();
                entity.ID = item.id;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public bool Delete(CityModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_City.Where(d=>d.id == entity.ID).FirstOrDefault();
                if(item != null)
                {
                    en.tbl_City.Remove(item);
                    return true;
                }
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public HashSet<CityModel> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<CityModel> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public CityModel FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<CityModel> GetAll()
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var items = (from city in en.tbl_City
                             select new CityModel
                             {
                                 ID = city.id,
                                 CityName = city.city_name
                             }).ToHashSet();
                return items;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new HashSet<CityModel>();
        }

        public HashSet<CityModel> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool Update(CityModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_City.Where(d=>d.id==entity.ID).FirstOrDefault();
                if(item != null)
                {
                    item.city_name = entity.CityName;
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
