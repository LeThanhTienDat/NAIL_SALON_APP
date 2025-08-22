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
    internal class DistrictRepository : IRepository<DistrictModel>
    {
        private static DistrictRepository _instance = null;
        public static DistrictRepository Instance
        {
            get
            {
                if( _instance == null)
                {
                    _instance = new DistrictRepository();
                }
                return _instance;
            }
        }
        public void Create(DistrictModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = new tbl_District
                {
                    district_name = entity.DistrictName
                };
                en.tbl_District.Add(item);
                en.SaveChanges();
                entity.ID = item.id;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public bool Delete(DistrictModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_District.Where(d => d.id == entity.ID).FirstOrDefault();
                if (item != null)
                {
                    en.tbl_District.Remove(item);
                    return true;
                }
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public HashSet<DistrictModel> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<DistrictModel> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public DistrictModel FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<DistrictModel> GetAll()
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var items = (from dis in en.tbl_District
                             select new DistrictModel
                             {
                                 ID = dis.id,
                                 DistrictName = dis.district_name,
                                 CityId = dis.city_id ?? null,
                             }).ToHashSet();
                return items;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new HashSet<DistrictModel>();
        }

        public HashSet<DistrictModel> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool Update(DistrictModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_District.Where(d=>d.id == entity.ID).FirstOrDefault();
                if (item != null)
                {
                    item.district_name = entity.DistrictName;
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
