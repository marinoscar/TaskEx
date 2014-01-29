using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx.Configuration
{
    [ConfigurationCollection(typeof(SettingConfiguration), AddItemName = "setting", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class SettingConfigurationCollection : ConfigurationCollectionBase<SettingConfiguration>
    {
    }
}
