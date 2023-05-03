using System.Data;
public static class UserManager
{
    public static int CurrentID { get; private set; }

    public static User? GetCurrentUser()
    {
        if (CurrentID == 0)
            return null;
        
        DataRow dr = DatabaseManager.QueryResult($"SELECT * FROM users WHERE id = {CurrentID}").Rows[0];
        return GetUser(dr);
    }

    public static User? GetUser(DataRow dr)
    {
        if (dr == null)
            return null;
        int id = (int)dr["id"];
        string email = (string)dr["email"];
        string firstName = (string)dr["first_name"];
        string lastName = (string)dr["last_name"];
        User user = new User(id, email, firstName, lastName);
        return user;
    }

    public static bool Login(string email, string password)
    {
        if (CurrentID != 0)
            return false;
        string inputPassword = HashedPassword(password);
        DataTable dt = DatabaseManager.QueryResult($"SELECT * FROM users WHERE email = '{email}' AND password = '{inputPassword}'");
        if (dt.Rows.Count < 1)
            return false;
        CurrentID = (int)dt.Rows[0]["id"];
        return true;
    }

    public static bool IsLoggedIn()
    {
        return CurrentID != 0;
    }

    public static void LogOut()
    {
        CurrentID = 0;
        // Clear the viewbag. Prevents the user from showing the wrong info when logging in again.
        LoginView.Instance.ClearViewBag();
    }

    public static string HashedPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}