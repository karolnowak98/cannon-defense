using System;

namespace GlassyCode.CannonDefense.Core.Utility.Static
{
    public static class TypeUtils
    {
        public static bool IsOfTypeOrDerived(Type type, Type targetType)
        {
            return type == targetType || type.IsSubclassOf(targetType);
        }
    }
}