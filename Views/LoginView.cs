/// <summary>
/// The view for the login menu. Singleton.
/// </summary>
public class LoginView : IView {
    /// <summary>
    /// The singleton instance.
    /// </summary>
    private static readonly LoginView instance = new LoginView();

    /// <summary>
    /// The viewbag. Holds temporary data for the view.
    /// </summary>
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    static LoginView() {
    }
    private LoginView() {
    }

    /// <summary>
    /// The getter for the singleton instance.
    /// </summary>
    public static LoginView Instance {
        get {
            return instance;
        }
    }

    
    /// <summary>
    /// Displays the login menu.
    /// </summary>
    public void Display() {
        ResetViewbag();
        List<string> optionsList = new List<string>() {
            $"Email: {ViewBag["email"]}",
            $"Password: {ViewBag["displaypassword"]}",
            "-",
            "Login",
            "Ga terug"
        };
        string[] options = optionsList.ToArray();
        Menu loginMenu = new Menu("Inloggen", options);
        int choice = loginMenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    /// <summary>
    /// Resets the viewbag to its default values.
    /// </summary>
    public void ResetViewbag()
    {
        ViewBag.Clear();
        if(!ViewBag.ContainsKey("email"))
            ViewBag["email"] = "<vul in>";
        if(!ViewBag.ContainsKey("password"))
            ViewBag["password"] = "";
        if(!ViewBag.ContainsKey("displaypassword"))
            ViewBag["displaypassword"] = "<vul in>";
    }
}
