using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWebAPIDemo.Common.Infrastructure.Extensions
{
    public static class CoreProfilerExtentions
    {
        public static string GetStepName<T>(this T SourceObj, [CallerMemberName] string callerName = "")
        {
            return (SourceObj == null) ? $"{callerName}" : $"{SourceObj?.GetType().Name}-{callerName}";
        }

        public static string GetCallingMethodName([CallerMemberName] string callerName = "")
        {
            return callerName;
        }
    }
}
