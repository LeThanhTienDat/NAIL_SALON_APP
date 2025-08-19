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
    internal class OrderDetailsRepository : IRepository<OrderDetailsModel>
    {
        private static OrderDetailsRepository _instance = null;
        public static OrderDetailsRepository Instance
        {
            get
            {
                if( _instance == null)
                {
                    _instance = new OrderDetailsRepository();
                }
                return _instance;
            }
        }
        public void Create(OrderDetailsModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = new tbl_OrderDetails
                {
                    service_id = entity.ServiceId,
                    order_id = entity.OrderId,
                };
                en.tbl_OrderDetails.Add(item);
                en.SaveChanges();
                entity.ID = item.id;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public bool Delete(OrderDetailsModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_OrderDetails.Where(d=>d.id == entity.ID).FirstOrDefault();
                if (item != null)
                {
                    en.tbl_OrderDetails.Remove(item);
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

        public HashSet<OrderDetailsModel> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<OrderDetailsModel> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public OrderDetailsModel FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<OrderDetailsModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public HashSet<OrderDetailsModel> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool Update(OrderDetailsModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_OrderDetails.Where(d=>d.id ==entity.ID).FirstOrDefault(); 
                if (item != null)
                {
                    item.service_id = entity.ServiceId;
                    item.order_id = entity.OrderId;
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
