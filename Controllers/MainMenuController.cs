public sealed class MainMenuController {
    private static readonly MainMenuController instance = new MainMenuController();

    static MainMenuController() {
    }
    private MainMenuController() {
    }

    public static MainMenuController Instance {
        get {
            return instance;
        }
    }

    public void ShowMainMenu() {
        MainMenuView.Instance.Display();
        try {
            int selectionInt = int.Parse(MainMenuView.Instance.ViewBag["MainMenuSelection"]);
            switch (selectionInt) {
            case 0:
                // Registreren
                break;
            case 1:
                // Inloggen
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
        catch (Exception e) {
            Console.WriteLine("Er is iets fout gegaan.");
            // return to main menu
        }
    }
}