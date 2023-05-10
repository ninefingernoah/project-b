public class Program {

    /// <summary>
    /// Main entry point for the application.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    public static void Main(string[] args) {
        DatabaseManager.CreateDatabase();
        var mainMenuController = MainMenuController.Instance;
        // mainMenuController.ShowMainMenu();
    }

}
