using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.Entities
{
    public class Category : BaseEntity
    {
        public List<Book> Books { get; set; }
        public bool isFeatured { get; set; }
    }
}
