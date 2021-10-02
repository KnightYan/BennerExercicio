namespace Benner.Microondas.Comuns.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            if (str == null)
                return true;
            return string.IsNullOrEmpty(str.Trim());
        }
    }
}