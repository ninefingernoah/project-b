/// <summary>
/// The controller for the main menu.
/// </summary>
public sealed class MainMenuController {

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    private static readonly MainMenuController instance = new MainMenuController();

    static MainMenuController() {
    }
    private MainMenuController() {
    }

    /// <summary>
    /// The singleton instance of the main menu controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static MainMenuController Instance {
        get {
            return instance;
        }
    }

    /// <summary>
    /// Shows the main menu and handles the user input.
    /// </summary>
    public void ShowMainMenu() {
        MainMenuView.Instance.Display();
        try {
            int selectionInt = int.Parse(MainMenuView.Instance.ViewBag["MainMenuSelection"]);
            switch (selectionInt) {
            case 0:
                // Registreren
                break;
            case 1:
                UserController.Instance.ShowLoginMenu();
                break;
            case 2:
                FlightListController.Instance.ShowFlights();
                break;
            case 3:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Ongeldige keuze.");
                break;
        }
        }
        catch (Exception) {
            Console.WriteLine("Er is iets fout gegaan.");
            // return to main menu
        }
    }
}