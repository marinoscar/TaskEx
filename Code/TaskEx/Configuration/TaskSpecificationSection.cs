using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx.Configuration
{
    public class TaskSpecificationSection : ConfigurationSection 
    {
        [ConfigurationProperty("specifications", IsRequired = true, IsDefaultCollection = false)]
        public SpecificationConfigurationCollection Specifications
        {
            get
            {
                return (SpecificationConfigurationCollection)this["specifications"] ??
                       new SpecificationConfigurationCollection();
            }
        }
    }
}
