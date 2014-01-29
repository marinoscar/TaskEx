using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEx
{
    public interface ISettings
    {
        Dictionary<string, object> Settings { get; }
        T GetSetting<T>(string name);
        T TryGetSetting<T>(string name, T defaultValue = default(T));
    }
}
