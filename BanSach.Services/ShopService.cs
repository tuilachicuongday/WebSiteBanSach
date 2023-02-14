﻿using BanSach.Database;
using BanSach.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.Services
{
    public class ShopService
    {
        public static ShopService Instance
        {
            get
            {
                if (instance == null) instance = new ShopService();
                return instance;
            }
        }
        private static ShopService instance { get; set; }
        private ShopService()
        {
        }
        public int SaveOrder(Order order)
        {
            using (var context = new BanSachContext())
            {
                context.Orders.Add(order);
                return context.SaveChanges();
            }
        }
    }
}
