using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CosmosMVC
{
    public class ProductSettings : ConfigurationSection
    {
        [ConfigurationProperty("DellSettings")]
        public DellFeatures DellFeatures
        {
            get
            {
                return (DellFeatures)this["DellSettings"];
            }
        }
    }
}