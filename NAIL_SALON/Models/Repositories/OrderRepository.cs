using NAIL_SALON.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models.Repositories
{
    internal class OrderRepository : IRepository<OrderModel>
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
        public void Create(OrderModel entity)
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
                    order_date = entity.OrderDate,
                    status = entity.Status,
                    payment_confirm = entity.PaymentConfirm ?? 0,
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

        public bool Delete(OrderModel entity)
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

        public HashSet<OrderModel> FindAll(string filter)
        {
            throw new NotImplementedException();
        }

        public HashSet<OrderModel> FindAllPaging(string filter, int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public OrderModel FindById(int id)
        {
            throw new NotImplementedException();
        }

        public HashSet<OrderModel> GetAll()
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var items = (
                    from order in en.tbl_Order

                    join customer in en.tbl_Customer on order.customer_id equals customer.id into customerGroup
                    from cus in customerGroup.DefaultIfEmpty()
                    join employer in en.tbl_Employer on order.employer_id equals employer.id into employerGroup
                    from em in employerGroup.DefaultIfEmpty()

                    join district in en.tbl_District on cus.district_id equals district.id into districtGroup
                    from dis in districtGroup.DefaultIfEmpty()
                    join city in en.tbl_City on cus.city_id equals city.id into cityGroup
                    from city in cityGroup.DefaultIfEmpty()

                    join ordetail in en.tbl_OrderDetails on order.id equals ordetail.order_id into orderDetailsGroup
                    from od in orderDetailsGroup.DefaultIfEmpty()
                    join service in en.tbl_Service on od.service_id equals service.id into serviceGroup
                    from ser in serviceGroup.DefaultIfEmpty()
                    join serviceProduct in en.tbl_ServiceProduct on ser.id equals serviceProduct.service_id into serviceProductGroup
                    from sp in serviceProductGroup.DefaultIfEmpty()
                    join product in en.tbl_Product on sp.product_id equals product.id into productGroup
                    from p in productGroup.DefaultIfEmpty()
                    join category in en.tbl_Category on p.category_id equals category.id into categoryGroup
                    from c in categoryGroup.DefaultIfEmpty()
                    orderby order.id descending
                    select new
                    {
                        order,
                        od,
                        ser,
                        sp,
                        p,
                        c,
                        cus,
                        em,
                        dis,
                        city
                    })
                    .AsEnumerable()
                    .GroupBy(x => x.order)
                    .Select(g => new OrderModel
                    {
                        ID = g.Key.id,
                        TotalPrice = g.Key.total_price ?? 0,
                        TotalDiscount = g.Key.total_discount ?? 0,
                        CustomerId = g.Key.customer_id,
                        EmployerId = g.Key.employer_id,
                        OrderDate = g.Key.order_date,
                        Status = g.Key.status,
                        PaymentConfirm = g.Key.payment_confirm ?? 0,
                        CurrentCustomer = g.FirstOrDefault().cus == null ? null : new CustomerModel
                        {
                            ID = g.FirstOrDefault().cus.id,
                            Name =g.FirstOrDefault().cus.name,
                            DistrictId = g.FirstOrDefault().cus.district_id,
                            DistrictName = g.FirstOrDefault().dis.district_name,
                            CityId = g.FirstOrDefault().cus.city_id,
                            CityName =g.FirstOrDefault().city.city_name,
                            Phone =g.FirstOrDefault().cus.phone,
                            Address =g.FirstOrDefault().cus.address,
                            BirthDay =g.FirstOrDefault().cus.birthday
                        },
                        CurrentEmployer = g.FirstOrDefault().em == null ? null : new EmployerModel
                        {
                            ID= g.FirstOrDefault().em.id,
                            Name=g.FirstOrDefault().em.name,
                            Phone =g.FirstOrDefault().em.phone,
                            Password =g.FirstOrDefault().em.password,
                            Email=g.FirstOrDefault().em.email,
                            Salt =g.FirstOrDefault().em.salt,
                            Active =g.FirstOrDefault().em.active ?? 0
                        },
                        OrderDetailsModel = new ObservableCollection<OrderDetailsModel>(
                            g.Where(x => x.od != null)
                            .GroupBy(x => x.od)
                            .Select(d => new OrderDetailsModel
                            {
                                ID = d.Key.id,
                                OrderId = d.Key.order_id,
                                ServiceId = d.Key.service_id ?? 0,
                                CurrentServiceBelong = d.FirstOrDefault().ser == null ? null : new ServiceModel
                                {
                                    ID = d.FirstOrDefault().ser.id,
                                    Name = d.FirstOrDefault().ser.name,
                                    Description = d.FirstOrDefault().ser.description,
                                    Discount = d.FirstOrDefault().ser.discount,
                                    Price = d.FirstOrDefault().ser.price,
                                    Active = d.FirstOrDefault().ser.active,
                                    DefaultProducts = new ObservableCollection<ServiceProductModel>(
                                        d
                                        .Where(x => x.sp != null)
                                        .Select(x => new ServiceProductModel
                                        {
                                            ServiceId = x.sp.service_id,
                                            ProductId = x.sp.product_id,
                                            Quantity = x.sp.quantity ?? 0,
                                            CurrentProductBelong = x.p == null ? null : new ProductModel
                                            {
                                                ID = x.p.id,
                                                Name = x.p.name,
                                                Description = x.p.description,
                                                Price = x.p.price,
                                                CategoryId = x.p.category_id,
                                                Stock = x.p.stock,
                                                Active = x.p.active,
                                                Image = x.p.image,
                                                CategoryName = x.c?.name
                                            }
                                        })
                                    )
                                }
                            })
                        )
                    }).ToHashSet();
                return items;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public HashSet<OrderModel> GetAllPaging(int index = 1, int pageSize = 10)
        {
            throw new NotImplementedException();
        }
        public HashSet<OrderModel> GetProcessingOrder(int index =1, int pageSize = 10)
        {
            try
            {
                var allOrders = this.GetAll();
                allOrders = allOrders.Where(d => d.Status.Equals("Processing")).ToHashSet();
                if (index < 1) index = 1;
                return allOrders
                    .Skip((index - 1) * pageSize)
                    .Take(pageSize)
                    .ToHashSet();
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }

        public bool Update(OrderModel entity)
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
                    item.payment_confirm = entity.PaymentConfirm;
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
        public bool CancelOrder(OrderModel entity)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = en.tbl_Order.Where(d => d.id == entity.ID).FirstOrDefault();
                if (item != null)
                {
                    item.status = "Canceled";
                    en.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }
        public OrderModel CheckOrder(int CusId)
        {
            try
            {
                DbNailSalon en = new DbNailSalon();
                var item = (from order in en.tbl_Order
                            where order.customer_id == CusId && order.status.Equals("Processing")
                            select new OrderModel
                            {
                                ID = order.id,
                                OrderDate = order.order_date,
                                EmployerId = order.employer_id,
                                CustomerId = order.customer_id,
                                PaymentConfirm = order.payment_confirm
                            }).FirstOrDefault();
                return item;
            }
            catch (EntityException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
