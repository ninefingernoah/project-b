public class LoginView : IView {
    private static readonly LoginView instance = new LoginView();
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    static LoginView() {
    }
    private LoginView() {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
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
        PopulateViewBag();
        // TODO: Is deze manier een beetje oke?
        List<string> optionsList = new List<string>() {
            $"Email: {ViewBag["email"]}",
            $"Password: {ViewBag["password"]}",
            "Login",
            "Ga terug"
        };
        string[] options = optionsList.ToArray();
        Menu loginMenu = new Menu("Inloggen", options);
        int choice = loginMenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    private void PopulateViewBag() {
        if(ViewBag["email"] == null)
            ViewBag["email"] = "<vul in>";
        if(ViewBag["password"] == null)
            ViewBag["password"] = "<vul in>";
    }

    public void ClearViewBag() {
        ViewBag.Clear();
    }
}
