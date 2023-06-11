using System.Text.RegularExpressions;
public static class StringUtils
{
    /// <summary>
    /// Checks if a string is a valid email address.
    /// </summary>
    /// <param name="email">The email to check.</param>
    /// <returns>True if the email is valid, false otherwise.</returns>
    public static bool CheckValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    }
}