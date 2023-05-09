using System.Data;
public static class UserManager
{
    /// <summary>
    /// The ID of the currently logged in user. 0 if no user is logged in.
    /// </summary>
    public static int CurrentID { get; private set; }

    /// <summary>
    /// Gets the currently logged in user. Returns null if no user is logged in.
    /// </summary>
    public static User? GetCurrentUser()
    {
        if (CurrentID == 0)
            return null;
        
        DataRow dr = DatabaseManager.QueryResult($"SELECT * FROM users WHERE id = {CurrentID}").Rows[0];
        return GetUser(dr);
    }

    /// <summary>
    /// Gets a user from a DataRow.
    /// </summary>
    /// <param name="dr">The DataRow to get the user from.</param>
    public static User? GetUser(DataRow dr)
    {
        if (dr == null)
            return null;
        int id = (int)(long)dr["id"];
        string email = (string)dr["email"];
        string firstName = (string)dr["first_name"];
        string lastName = (string)dr["last_name"];
        User user = new User(id, email, firstName, lastName);
        return user;
    }

    /// <summary>
    /// Logs the user in. Returns true if the login was successful, false if not.
    /// </summary>
    public static bool Login(string email, string password)
    {
        if (CurrentID != 0)
            return false;
        DataTable dt = DatabaseManager.QueryResult($"SELECT * FROM users WHERE email = '{email}';");
        if (dt.Rows.Count == 0)
            return false;
        string dbPassword = (string)dt.Rows[0]["password"];
        if (!BCrypt.Net.BCrypt.Verify(password, dbPassword))
            return false;
        CurrentID = (int)(long)dt.Rows[0]["id"];
        return true;
    }

    /// <summary>
    /// Checks if a user is logged in.
    /// </summary>
    public static bool IsLoggedIn()
    {
        return CurrentID != 0;
    }

    /// <summary>
    /// Logs the user out.
    /// </summary>
    public static void LogOut()
    {
        CurrentID = 0;
        // Clear the viewbag. Prevents the user from showing the wrong info when logging in again.
        LoginView.Instance.ClearViewBag();
    }

    /// <summary>
    /// Hashes a password.
    /// </summary>
    public static string HashedPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <summary>
    /// Registers a user.
    /// </summary>
    /// <param name="fname">The first name of the user.</param>
    /// <param name="lname">The last name of the user.</param>
    /// <param name="email">The email of the user.</param>
    /// <param name="pass">The password of the user.</param>
    public static void Register(string fname, string lname, string email, string pass)
    {
        if (UserExists(email))
        {
            ConsoleUtils.Error("Een gebruiker met dit emailadres bestaat al.", RegisterController.Instance.ShowRegisterMenu);
            return;
        }
        string hashedPass = HashedPassword(pass);
        DatabaseManager.QueryNonResult($"INSERT INTO users (first_name, last_name, role, email, password) VALUES ('{fname}', '{lname}', 'USER', '{email}', '{hashedPass}');");
        if (!Login(email, pass))
        {
            throw new Exception("Failed to login after registering.");
        }
    }

    /// <summary>
    /// Checks if a user exists.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    public static bool UserExists(string email)
    {
        DataTable dt = DatabaseManager.QueryResult($"SELECT * FROM users WHERE email = '{email}';");
        return dt.Rows.Count > 0;
    }
}