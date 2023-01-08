using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ConfigurationHelper
    {
        public static TResult GetValue<TResult>(string key)
        {
            var setting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(setting))
                return default(TResult);
            var result = Convert.ChangeType(setting, typeof(TResult));
            return (TResult)result;
        }
    }
}
