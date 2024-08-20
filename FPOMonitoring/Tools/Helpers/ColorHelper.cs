using FPOMonitoring.Data.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Tools.Helpers
{
    public static class ColorHelper
    {
        public static string GetColor(ColorsEnum color)
        {
            var field = typeof(ColorsEnum).GetField(color.ToString());
            var attribute = (ColorValueAttribute)Attribute.GetCustomAttribute(field, typeof(ColorValueAttribute));

            return attribute?.Value;
        }
    }
}
