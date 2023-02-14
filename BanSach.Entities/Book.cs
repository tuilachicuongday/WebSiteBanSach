using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.Entities
{
    public class Book : BaseEntity
    {
        public decimal Price { get; set; }

        public virtual Category Category { get; set; }

        public string ImageURL { get; set; }
        public int CategoryID { get; set; }
        public string Author { get; set; }
    }
}
