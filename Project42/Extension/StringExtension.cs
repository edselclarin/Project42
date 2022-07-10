namespace Project42.Extension
{
    public static class StringExtension
    {
        public static string ToCSV(this string? inputText)
        {
            if (String.IsNullOrEmpty(inputText))
            {
                return String.Empty;
            }
            else
            {
                var separators = new List<char>();
                if (inputText.Contains('\r') || inputText.Contains('\n'))
                {
                    separators.Add('\r');
                    separators.Add('\n');
                }
                else if (inputText.Contains('\t'))
                {
                    separators.Add('\t');
                }
                else
                {
                    separators.Add(' ');
                }

                var items = inputText.Split(separators.ToArray(), StringSplitOptions.RemoveEmptyEntries);

                return String.Join(",", items);
            }
        }
    }
}
