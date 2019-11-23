using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CosmosMVC
{
    public class DellFeatures : ConfigurationElement
    {
        // <view name = "AccountClassificationAdmin" type="MainUi.AccountClassification.AccountClassificationAdminForm, , Version=1.0.0.0, Culture=neutral, PublicKeyToken=null" controller="" title="Account Classification Admin"
        [ConfigurationProperty("ProductNumber", DefaultValue = 00000, IsRequired = true)]
        public int ProductNumber
        {
            get
            {
                return (int)this["ProductNumber"];
            }
        }

        [ConfigurationProperty("ProductName", DefaultValue = "DELL", IsRequired = true)]
        public string ProductName
        {
            get
            {
                return (string)this["ProductName"];
            }
        }

        [ConfigurationProperty("Color", IsRequired = false)]
        public string Color
        {
            get
            {
                return (string)this["Color"];
            }
        }
        [ConfigurationProperty("Warranty", DefaultValue = "1 Years", IsRequired = false)]
        public string Warranty
        {
            get
            {
                return (string)this["Warranty"];
            }
        }
    }

}