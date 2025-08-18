using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models
{
    internal class Admin
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone {  get; set; }
        public string Password { get; set; }
        public int Active { get; set; }
        public string Salt { get; set; }
    }
}
