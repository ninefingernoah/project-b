/// <summary>
/// The view for the main menu. Singleton.
/// </summary>
public class MainMenuView : IView {
    /// <summary>
    /// The singleton instance.
    /// </summary>
    private static readonly MainMenuView instance = new MainMenuView();

    /// <summary>
    /// The viewbag. Holds temporary data for the view.
    /// </summary>
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    /// <summary>
    /// The getter for the singleton instance.
    /// </summary>
    public static MainMenuView Instance {
        get {
            return instance;
        }
    }

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
            "Boeking bekijken",
            "Vliegveldinformatie",
            "Afsluiten"
        };
        string[] options = optionsList.ToArray();
        Menu mainMenu = new Menu("Main Menu", options);
        int choice = mainMenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }
}
