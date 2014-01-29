using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx.Configuration
{
    public class SettingConfiguration : ConfigurationBase
    {
        [ConfigurationProperty("value", IsRequired = true, IsKey = true)]
        public string Value
        {
            get { return Convert.ToString(this["value"]); }
            set { this["value"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return Convert.ToString(this["value"]); }
            set { this["type"] = value; }
        }

        public object GetValue()
        {
            var type = Type.ToLower();
            switch (type)
            {
                case "number":
                    return Convert.ToDouble(Value);
                case "boolean":
                    return Convert.ToBoolean(Value);
                case "fileContent":
                    return GetFileContent();
                default:
                    return Value;
            }
        }

        private string GetFileContent()
        {
            string result;
            using (var stream = new StreamReader(Value))
            {
                result = stream.ReadToEnd();
                stream.Close();
            }
            return result;
        }
    }
}
