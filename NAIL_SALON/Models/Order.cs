using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models
{
    internal class Order
    {
        public int ID { get; set; }
        public int CustomerId {  get; set; }
        public int EmployerId { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalDiscount { get; set; }
        public DateTime OrderDate { get; set; } 
    }
}
