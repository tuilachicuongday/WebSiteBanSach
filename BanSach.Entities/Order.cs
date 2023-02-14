using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.Entities
{
    public class Order 
    {
        public int ID { get; set; }
        public string UserID { get; set; }

        public DateTime OrderAt { get; set; }

        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        public  virtual List<OrderItem> OrderItems { get; set; }

    }
}
