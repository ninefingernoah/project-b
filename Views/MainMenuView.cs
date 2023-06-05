public class MainMenuView : IView {
    private static readonly MainMenuView instance = new MainMenuView();
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    static MainMenuView() {
    }
    private MainMenuView() {
    }

    /// <summary>
    /// Displays the main menu.
    /// </summary>
    public void Display() {
        List<string> optionsList = new List<string>() {
            "Registreren",
            "Inloggen",
            "Boek vlucht",
            "Zoek vlucht",
            "Boeking bekijken",
            "Stoel selectie test",
            "Vliegveldinformatie",
            "Afsluiten"
        };
        string[] options = optionsList.ToArray();
        Menu mainMenu = new Menu("Main Menu", options);
        int choice = mainMenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static MainMenuView Instance {
        get {
            return instance;
        }
    }
}
