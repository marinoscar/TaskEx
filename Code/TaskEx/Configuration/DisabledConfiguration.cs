using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx.Configuration
{
    public class DisabledConfiguration : ConfigurationBase
    {
        [ConfigurationProperty("disabled", IsRequired = false, DefaultValue = false)]
        public bool Disabled
        {
            get { return Convert.ToBoolean(this["disabled"]); }
            set { this["disabled"] = value; }
        }
    }
}
