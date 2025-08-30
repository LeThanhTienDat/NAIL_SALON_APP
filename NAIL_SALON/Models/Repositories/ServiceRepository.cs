using NAIL_SALON.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

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
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = (from ser in en.tbl_Service
                            where ser.id == id
                            join serpro in en.tbl_ServiceProduct on ser.id equals serpro.service_id into serproGroup
                            from sp in serproGroup.DefaultIfEmpty()
                            join pro in en.tbl_Product on sp != null ? sp.product_id : 0 equals pro.id into proGroup
                            from pr in proGroup.DefaultIfEmpty()
                            join cate in en.tbl_Category on pr != null ? pr.category_id : 0 equals cate.id into proCategoryGroup
                            from ca in proCategoryGroup.DefaultIfEmpty()                            
                            orderby ser.id descending
                            select new
                            {
                                ser,
                                sp,
                                pr,
                                ca
                            })
                             .AsEnumerable()
                             .GroupBy(x => x.ser)
                             .Select(g => new ServiceModel
                             {
                                 ID = g.Key.id,
                                 Name = g.Key.name,
                                 Description = g.Key.description,
                                 Price = g.Key.price ?? 0,
                                 Active = g.Key.active ?? 0,
                                 Discount = g.Key.discount ?? 0,
                                 ServiceProductModel = g.Where(x => x.sp != null)
                                    .Select(x => new ServiceProductModel
                                    {
                                        ServiceId = x.sp.service_id,
                                        ProductId = x.sp.product_id,
                                        Quantity = x.sp.quantity ?? 0,
                                        CurrentProductBelong = x.pr == null ? null : new ProductModel
                                        {
                                            ID = x.pr.id,
                                            Name = x.pr.name,
                                            Description = x.pr.description,
                                            Price = x.pr.price ?? 0,
                                            CategoryId = x.pr.category_id,
                                            Stock = x.pr.stock ?? 0,
                                            Active = x.pr.active ?? 0,
                                            Image = x.pr.image,
                                            CategoryName = x.ca?.name
                                        }
                                    }).ToHashSet()
                             })
                             .FirstOrDefault();                            
                return item;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new ServiceModel();
        }

        public HashSet<ServiceModel> GetAll()
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var items = (from ser in en.tbl_Service
                             join serpro in en.tbl_ServiceProduct on ser.id equals serpro.service_id into serproGroup
                             from sp in serproGroup.DefaultIfEmpty()
                             join pro in en.tbl_Product on sp != null ? sp.product_id : 0 equals pro.id into proGroup
                             from pr in proGroup.DefaultIfEmpty()
                             join cate in en.tbl_Category on pr!=null ? pr.category_id : 0 equals cate.id into proCategoryGroup
                             from ca in proCategoryGroup.DefaultIfEmpty()
                             where !ser.name.Equals("accompanying product")
                             orderby ser.id descending
                             select new
                             {
                                 ser, sp, pr, ca
                             })
                             .AsEnumerable()
                             .GroupBy(x => x.ser)
                             .Select(g => new ServiceModel
                             {
                                 ID = g.Key.id,
                                 Name = g.Key.name,
                                 Description = g.Key.description,
                                 Price = g.Key.price ?? 0,
                                 Active = g.Key.active ?? 0,
                                 Discount = g.Key.discount ?? 0,
                                 ServiceProductModel = g.Where(x => x.sp != null)
                                    .Select(x => new ServiceProductModel
                                    {
                                        ServiceId = x.sp.service_id,
                                        ProductId = x.sp.product_id,
                                        Quantity = x.sp.quantity ?? 0,
                                        CurrentProductBelong = x.pr == null ? null : new ProductModel
                                        {
                                            ID = x.pr.id,
                                            Name = x.pr.name,
                                            Description = x.pr.description,
                                            Price = x.pr.price ?? 0,
                                            CategoryId = x.pr.category_id,
                                            Stock = x.pr.stock ?? 0,
                                            Active = x.pr.active ?? 0,
                                            Image = x.pr.image,
                                            CategoryName = x.ca?.name
                                        }
                                    }).ToHashSet()
                             })
                             .OrderByDescending(ser => ser.ID)
                             .ToHashSet();                            
                return items;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new HashSet<ServiceModel>();
        }

        public HashSet<ServiceModel> GetAllPaging(int index = 1, int pageSize = 10)
        {
            try
            {
                var allServices = this.GetAll();
                if (index < 1) index = 1;
                return allServices
                        .Skip((index - 1) * pageSize)
                        .Take(pageSize)
                        .ToHashSet();
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return new HashSet<ServiceModel>();
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
        public bool ChangeActive(ServiceModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Service.Where(d => d.id == entity.ID).FirstOrDefault();
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
