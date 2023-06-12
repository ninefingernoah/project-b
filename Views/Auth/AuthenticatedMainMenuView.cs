/// <summary>
/// View for displaying the main menu for authenticated users. Singleton.
/// </summary>
public class AuthenticatedMainMenuView : IView {
    /// <summary>
    /// Singleton instance
    /// </summary>
    private static readonly AuthenticatedMainMenuView instance = new AuthenticatedMainMenuView();
    /// <summary>The viewbag. Holds temporary data for the view.</summary>
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    static AuthenticatedMainMenuView() {
    }
    private AuthenticatedMainMenuView() {
    }

    /// <summary>
    /// Displays the main menu.
    /// </summary>
    public void Display() {
        List<string> optionsList = new List<string>() {
            "Bekijk reserveringen",
            "Boek vlucht",
            "Vliegveldinformatie",
            "-",
            "Uitloggen"
        };
        string[] options = optionsList.ToArray();
        Menu mainMenu = new Menu($"Welkom {UserManager.GetCurrentUser()!.FirstName}", options);
        int choice = mainMenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static AuthenticatedMainMenuView Instance {
        get {
            return instance;
        }
    }
}
