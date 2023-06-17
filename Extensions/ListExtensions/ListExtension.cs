namespace Extensions.ListExtensions;

public static class ListExtension
{
    public static bool IsNullOrEmpty<T>(this List<T> list) => list == null || list.Count == 0;
    
    public static bool IsNotNullOrEmpty<T>(this List<T> list) => !IsNullOrEmpty(list);

    public static bool IsNullOrEmpty<T>(this T[] array) => array == null || array.Length == 0;

    public static bool IsNotNullOrEmpty<T>(this T[] array) => !IsNullOrEmpty(array);
}
