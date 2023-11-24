
using Farkle.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farkle.Entities.CustomEventArgs
{
    public class SettingValueEventArgs : EventArgs
    {
        public FarkleSettingType SettingType { get; set; }
        public object SettingValue { get; set; }

        public string SettingName { get; set; }

        public T ParseValue<T>()
        {
            try
            {
                return (T)SettingValue;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
