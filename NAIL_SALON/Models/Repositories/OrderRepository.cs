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
    internal class OrderRepository : IRepository<Order>
    {
        private static OrderRepository _instance= null;
        public static OrderRepository Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new OrderRepository();
                }
                return _instance;
            }
        }
        public void Create(Order entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = new tbl_Order
                {
                    total_price = entity.TotalPrice,
                    total_discount = entity.TotalDiscount,
                    customer_id = entity.CustomerId,
                    employer_id = entity.EmployerId,
                    order_date = entity.OrderDate
                };
                en.tbl_Order.Add(item);
                en.SaveChanges();
                entity.ID = item.id;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public bool Delete(Order entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Order.Where(d=> d.id == entity.ID).FirstOrDefault();
                if (item != null)
                {
                    en.tbl_Order.Remove(item);
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

        public HashSet<Order> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<Order> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public Order FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public HashSet<Order> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool Update(Order entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Order.Where(d=>d.id == entity.ID).FirstOrDefault();
                if (item != null)
                {
                    item.total_price = entity.TotalPrice;
                    item.total_discount = entity.TotalDiscount;
                    item.customer_id = entity.CustomerId;
                    item.employer_id = entity.EmployerId;
                    item.order_date = entity.OrderDate;
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
