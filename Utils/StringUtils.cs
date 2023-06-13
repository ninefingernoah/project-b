using System.Text.RegularExpressions;
public static class StringUtils
{
    public static bool CheckValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
    }

    // public static bool IsValidHouseNumber(string number)
    // {
    //     // Is the first part numeric?
    //     // Find out where the first non-numeric character is.
    //     if(number.Length == 0 || number == null || number.Length > 6)
    //     {
    //         return false;
    //     }
    //     int index = 0;
    //     foreach (char c in number)
    //     {
    //         if (!char.IsDigit(c))
    //         {
    //             break;
    //         }
    //         index++;
    //     }
    //     string firstpart = number.Substring(index);
    //     if(firstpart.Length == 0)
    //     {
    //         return false;
    //     }
    //     // Is the first part numeric?
    //     if (!int.TryParse(firstpart, out int result))
    //     {
    //         return false;
    //     }
    //     return true;
    // }
}