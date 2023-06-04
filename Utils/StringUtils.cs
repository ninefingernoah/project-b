using System.Text.RegularExpressions;
public static class StringUtils
{
    public static bool CheckValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    }
}