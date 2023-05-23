public class RegisterView : IView {
    private static readonly RegisterView instance = new RegisterView();
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    static RegisterView() {
    }
    private RegisterView() {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static RegisterView Instance {
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
            $"Voornaam: {ViewBag["firstname"]}",
            $"Achternaam: {ViewBag["lastname"]}",
            $"Email: {ViewBag["email"]}",
            $"Wachtwoord: {ViewBag["displaypassword"]}",
            $"Bevestig wachtwoord: {ViewBag["cdisplaypassword"]}",
            "-",
            "Registreer",
            "Ga terug"
        };
        string[] options = optionsList.ToArray();
        Menu registermenu = new Menu("Registreren", options);
        int choice = registermenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    private void PopulateViewBag() {
        if(!ViewBag.ContainsKey("email"))
            ViewBag["email"] = "<vul in>";
        if(!ViewBag.ContainsKey("password"))
            ViewBag["password"] = "";
        if(!ViewBag.ContainsKey("displaypassword"))
            ViewBag["displaypassword"] = "<vul in>";
        if(!ViewBag.ContainsKey("cpassword"))
            ViewBag["cpassword"] = "";
        if(!ViewBag.ContainsKey("cdisplaypassword"))
            ViewBag["cdisplaypassword"] = "<vul in>";
        if(!ViewBag.ContainsKey("firstname"))
            ViewBag["firstname"] = "<vul in>";
        if(!ViewBag.ContainsKey("lastname"))
            ViewBag["lastname"] = "<vul in>";
    }

    public void ClearViewBag() {
        ViewBag.Clear();
    }
}
