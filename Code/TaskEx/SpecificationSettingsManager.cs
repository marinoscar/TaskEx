using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskEx.Configuration;

namespace TaskEx
{
    public class SpecificationSettingsManager : SettingsManager
    {
        public SpecificationSettingsManager(ISpecification spec)
            : base(GetSettings(spec))
        {
        }

        private static SettingConfigurationCollection GetSettings(ISpecification spec)
        {
            var configManager = new TaskConfigurationManager();
            var specConfig = configManager.GetSpecification(spec.Name);
            return specConfig.Settings;
        }
    }
}
