using System;

namespace Pin.Tests
{
    public static class StringUtilities
    {
        public static string[] Replace(this string[] array, string oldString,string newString = "")
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i].Replace(oldString, newString);
            }
            return array;
        }
    }
}
