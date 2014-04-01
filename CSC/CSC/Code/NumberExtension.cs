namespace CSC
{
    public static class NumberExtension
    {
        public static string ToNString(this long? number)
        {
            if (number.HasValue)
                return number.Value.ToString("N0");
            return string.Empty;
        }

        public static string ToNString(this int? number)
        {
            if (number.HasValue)
                return number.Value.ToString("N0");
            return string.Empty;
        }

        public static string ToNString(this decimal? number,bool hidePoint=false)
        {
            if (number.HasValue)
            {
                if (hidePoint)
                    return number.Value.ToString("N0");
                return number.Value.ToString("N2");
            }

            return string.Empty;
        }
    }
}