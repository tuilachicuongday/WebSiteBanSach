using BanSach.Entities;
using BanSach.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanSach.Web.ViewModels
{
    public class OrderViewModel
    {
        public List<Order> Orders { get; set; }
        public string UserID { get;  set; }
        public Pager Pager { get;  set; }
        public string Status { get;  set; }
    }
    public class OrderDetailViewModel
    {
        public Order Order { get; set; }
        public ApplicationUser User { get; set; }
        public List<string> AvailableStatus { get; set; }
    }
}