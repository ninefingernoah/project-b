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
        if(UserManager.IsLoggedIn()) {
            ShowAuthenticatedMainMenu();
        } else {
            ShowUnauthenticatedMainMenu();
        }
    }

    /// <summary>
    /// Shows the unauthenticated main menu and handles the user input.
    /// </summary>
    private void ShowUnauthenticatedMainMenu()
    {
        MainMenuView.Instance.Display();
        try {
            int selectionInt = int.Parse(MainMenuView.Instance.ViewBag["MainMenuSelection"]);
            switch (selectionInt) {
            case 0:
                RegisterController.Instance.ShowRegisterMenu();
                break;
            case 1:
                LoginController.Instance.ShowLoginMenu();
                break;
            case 2:
                ReservationController.Instance.Start();
                break;
            case 3:
                break;
            default:
                Console.WriteLine("Ongeldige keuze.");
                break;
        }
        }
        catch (Exception) {
            ConsoleUtils.Error("Er is iets fout gegaan.");
            ShowMainMenu();
            // return to main menu
        }
    }

    /// <summary>
    /// Shows the authenticated main menu and handles the user input.
    /// </summary>
    private void ShowAuthenticatedMainMenu()
    {
        AuthenticatedMainMenuView.Instance.Display();
        try
        {
            int selection = int.Parse(AuthenticatedMainMenuView.Instance.ViewBag["MainMenuSelection"]);
            switch (selection)
            {
                case 0:
                    // FlightListController.Instance.ShowFlights();
                    break;
                case 1:
                    // View account info
                    break;
                case 3:
                    // Log out
                    UserManager.LogOut();
                    ConsoleUtils.Success("U bent uitgelogd.");
                    ShowUnauthenticatedMainMenu();
                    break;
            }
        }
        catch (Exception)
        {
            ConsoleUtils.Error("Er is iets fout gegaan.");
            ShowMainMenu();
        }
    }
    
}