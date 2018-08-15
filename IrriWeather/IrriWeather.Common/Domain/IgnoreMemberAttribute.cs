using System;
using System.Collections.Generic;
using System.Text;

namespace IrriWeather.Common.Domain
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class IgnoreMemberAttribute : Attribute
    {
    }
}
