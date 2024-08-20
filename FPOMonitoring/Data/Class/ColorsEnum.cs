using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Data.Class
{
    public enum ColorsEnum
    {
        [ColorValue("#174189")]
        Primary,

        [ColorValue("#dad7cd")]
        Secondary,

        [ColorValue("#d62828")]
        Danger,

        [ColorValue("#f77f00")]
        Warning,

        [ColorValue("#29bf12")]
        Success,
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class ColorValueAttribute : Attribute
    {
        public string Value { get; }

        public ColorValueAttribute(string value)
        {
            Value = value;
        }
    }

   
}
