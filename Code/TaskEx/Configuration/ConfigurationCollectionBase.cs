using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx.Configuration
{
    public class ConfigurationCollectionBase<T> : ConfigurationElementCollection where T : ConfigurationBase
    {

        public T this[int index]
        {
            get
            {
                return base.BaseGet(index) as T;
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return Activator.CreateInstance<T>();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((T)element).Name;
        }
    }
}
