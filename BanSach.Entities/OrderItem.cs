using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.Entities
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int Quantity { get; set; }
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
        public int BookID { get; set; }
        public virtual Book Book { get; set; }
    }
}
