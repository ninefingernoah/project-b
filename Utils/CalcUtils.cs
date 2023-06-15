public static class CalcUtils
{
    /// <summary>
    /// Calculates the amount of characters that are different between two strings.
    /// </summary>
    /// <param name="oldString">The old string.</param>
    /// <param name="newString">The new string.</param>
    /// <returns>The amount of characters.</returns>
    public static int DeltaString(string oldString, string newString)
    {
        int count = 0;

        // Determine the maximum length of the strings
        int maxLength = Math.Max(oldString.Length, newString.Length);

        // Iterate over each character at the corresponding index
        for (int i = 0; i < maxLength; i++)
        {
            // Check if the index is valid for both strings
            if (i < oldString.Length && i < newString.Length)
            {
                // Compare characters at the same index
                if (oldString[i] != newString[i])
                {
                    count++;
                }
            }
            else
            {
                // If one of the strings has fewer characters, count them as changed
                count++;
            }
        }

        return count;
    }
}