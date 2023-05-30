public class AdminMainMenuView : IView {
    private static readonly AdminMainMenuView instance = new AdminMainMenuView();
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    static AdminMainMenuView() {
    }
    private AdminMainMenuView() {
    }

    /// <summary>
    /// Displays the main menu.
    /// </summary>
    public void Display() {
        List<string> optionsList = new List<string>() {
            "Vlucht toevoegen",
            "Vlucht wijzigen/verwijderen",
            "Vliegveld toevoegen",
            "Uitloggen"
        };
        string[] options = optionsList.ToArray();
        Menu mainMenu = new Menu("Admin Menu", options);
        int choice = mainMenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static AdminMainMenuView Instance {
        get {
            return instance;
        }
    }
}
