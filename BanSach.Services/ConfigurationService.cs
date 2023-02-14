using BanSach.Database;
using BanSach.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BanSach.Services
{
    public class ConfigurationService
    {
        public static ConfigurationService Instance
        {
            get
            {
                if (instance == null) instance = new ConfigurationService();
                return instance;
            }
        }

        public int ShopPageSize()
        {
            using (var context = new BanSachContext())
            {
                var pageSizeConfig = context.Configurations.Find("ShopPageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 6;
            }
        }

        public Config GetConfig(string Key)
        {
            using (var context = new BanSachContext())
            {
                return context.Configurations.Find(Key);
            }
        }
        private static ConfigurationService instance { get; set; }
        private ConfigurationService()
        {
        }
        public int PageSize()
        {
            using (var context = new BanSachContext())
            {
                var pageSizeConfig = context.Configurations.Find("PageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 5;
            }
        }
    }
}
