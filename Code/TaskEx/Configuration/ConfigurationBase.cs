using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx.Configuration
{
    public class ConfigurationBase : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return Convert.ToString(this["name"]); }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("description")]
        public string Description
        {
            get { return Convert.ToString(this["description"]); }
            set { this["description"] = value; }
        }

        [ConfigurationProperty("order", IsRequired = false, DefaultValue = 99)]
        public int Order
        {
            get { return Convert.ToInt32(this["order"]); }
            set { this["order"] = value; }
        }
    }
}
