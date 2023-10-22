using System;
using System.Collections.Generic;
public static class TypeCast
{
    #region Convertors
    public static T ToType<T>(this object val, T alt = default(T)) where T : struct, IConvertible
    {
        try
        {
            return (T)Convert.ChangeType(val, typeof(T));
        }
        catch
        {
            return alt;
        }
    }
    public static T? ToTypeOrNull<T>(this object val, T? alt = null) where T : struct, IConvertible
    {
        try
        {
            return (T)Convert.ChangeType(val, typeof(T));
        }
        catch
        {
            return alt;
        }
    }    
    #endregion

    public static bool IsBetween<T>(this T item, T start, T end)
    {
        return Comparer<T>.Default.Compare(item, start) >= 0
            && Comparer<T>.Default.Compare(item, end) <= 0;
    }

}

