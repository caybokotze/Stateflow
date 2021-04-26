using System;
using System.Diagnostics;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace Stateflow
{
    public static class ClassHelper
    {
        public static string GetNameOfCallingClass()
        {
            string fullName;
            Type declaringType;
            int skipFrames = 2;
            do
            {
                MethodBase method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    return method.Name;
                }
                skipFrames++;
                fullName = declaringType.Name;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));
            return fullName;
        }
    }
}