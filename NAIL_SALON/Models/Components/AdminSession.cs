using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAIL_SALON.Models.Components
{
    internal class AdminSession
    {
        public bool IsAdmin { get; set; }
        public Admin CurrentUser { get; set; }
    }
}
