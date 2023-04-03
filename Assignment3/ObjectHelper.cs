public static class ObjectHelper
{
    public static TType? CastOrDefault<TType>(this object value)
        where TType : struct
    {
        try
        {
            return (TType)Convert.ChangeType(value, typeof(TType));
        }
        catch (Exception)
        {
            return default;
        }
    }
}
