public class MainMenuView {
    private static readonly MainMenuView instance = new MainMenuView();
    public Dictionary<string, string> ViewBag = new Dictionary<string, string>();

    static MainMenuView() {
    }
    private MainMenuView() {
    }

    public void Display() {
        List<string> optionsList = new List<string>() {
            "Registreren",
            "Inloggen",
            "Zoek vlucht",
            "Afsluiten"
        };
        string[] options = optionsList.ToArray();
        Menu mainMenu = new Menu("Main Menu", options);
        int choice = mainMenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    public static MainMenuView Instance {
        get {
            return instance;
        }
    }
}