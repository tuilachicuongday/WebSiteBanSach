using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        [Required]
        [MinLength(5), MaxLength(100)]
        public string Name { get; set; }
        [MinLength(5)]
        public string Description { get; set; }
    }
}
