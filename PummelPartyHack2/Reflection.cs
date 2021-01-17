using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PummelPartyHack
{
    public static class Reflection
    {
        public static void SetProperty(object instance, string propertyName, object newValue)
        {
            Type type = instance.GetType();

            PropertyInfo prop = type.BaseType.GetProperty(propertyName);

            prop.SetValue(instance, newValue, null);
        }
    }
}
