/// <summary>
/// The view for the register menu. Singleton.
/// </summary>
public class RegisterView : IView
{
    /// <summary>
    /// The singleton instance.
    /// </summary>
    private static readonly RegisterView instance = new RegisterView();

    /// <summary>
    /// The viewbag. Holds temporary data for the view.
    /// </summary>
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    static RegisterView()
    {
    }
    private RegisterView()
    {
    }

    /// <summary>
    /// The getter for the singleton instance.
    /// </summary>
    public static RegisterView Instance
    {
        get
        {
            return instance;
        }
    }


    /// <summary>
    /// Displays the login menu.
    /// </summary>
    public void Display()
    {
        PopulateViewBag();
        List<string> optionsList = new List<string>() {
            $"Voornaam: {ViewBag["firstname"]}",
            $"Achternaam: {ViewBag["lastname"]}",
            $"Email: {ViewBag["email"]}",
            $"Wachtwoord: {ViewBag["displaypassword"]}",
            $"Bevestig wachtwoord: {ViewBag["cdisplaypassword"]}",
            "-",
            "Registreer",
            "Terug"
        };
        string[] options = optionsList.ToArray();
        Menu registermenu;
        // Holds the last choice made by the user. This way the cursor will be on the last choice made by the user.
        if (ViewBag.ContainsKey("MainMenuSelection"))
        {
            registermenu = new Menu("Registreren", options, int.Parse((string)ViewBag["MainMenuSelection"]));
        }
        else
        {
            registermenu = new Menu("Registreren", options);
        }
        int choice = registermenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    // /// <summary>
    // /// Resets the viewbag to its default values.
    // /// </summary>
    // public void ResetViewBag()
    // {
    //     ViewBag.Clear();

    //     if(!ViewBag.ContainsKey("email"))
    //         ViewBag["email"] = "<vul in>";
    //     if(!ViewBag.ContainsKey("password"))
    //         ViewBag["password"] = "";
    //     if(!ViewBag.ContainsKey("displaypassword"))
    //         ViewBag["displaypassword"] = "<vul in>";
    //     if(!ViewBag.ContainsKey("cpassword"))
    //         ViewBag["cpassword"] = "";
    //     if(!ViewBag.ContainsKey("cdisplaypassword"))
    //         ViewBag["cdisplaypassword"] = "<vul in>";
    //     if(!ViewBag.ContainsKey("firstname"))
    //         ViewBag["firstname"] = "<vul in>";
    //     if(!ViewBag.ContainsKey("lastname"))
    //         ViewBag["lastname"] = "<vul in>";
    // }

    public void ClearViewBag()
    {
        ViewBag.Clear();
    }

    public void PopulateViewBag()
    {
        if (!ViewBag.ContainsKey("email"))
            ViewBag["email"] = "<vul in>";
        if (!ViewBag.ContainsKey("password"))
            ViewBag["password"] = "";
        if (!ViewBag.ContainsKey("displaypassword"))
            ViewBag["displaypassword"] = "<vul in>";
        if (!ViewBag.ContainsKey("cpassword"))
            ViewBag["cpassword"] = "";
        if (!ViewBag.ContainsKey("cdisplaypassword"))
            ViewBag["cdisplaypassword"] = "<vul in>";
        if (!ViewBag.ContainsKey("firstname"))
            ViewBag["firstname"] = "<vul in>";
        if (!ViewBag.ContainsKey("lastname"))
            ViewBag["lastname"] = "<vul in>";
    }

}
