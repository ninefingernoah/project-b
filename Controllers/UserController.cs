/// <summary>
/// The controller for the user menus.
/// </summary>
public sealed class UserController {

    /// <summary>
    /// The singleton instance of the user menus controller. Used for accessing the controller. Thread safe.
    /// </summary>
    private static readonly UserController instance = new UserController();

    static UserController() {
    }
    private UserController() {
    }

    /// <summary>
    /// The singleton instance of the user menus controller. Used for accessing the controller. Thread safe.
    /// </summary>
    public static UserController Instance {
        get {
            return instance;
        }
    }

    public void ShowReservations()
    {
        int numberOfReservations = UserManager.GetCurrentUser().GetReservationCount();
        if(numberOfReservations < 1)
        {
            ConsoleUtils.Error("U heeft nog geen reserveringen.");
            MainMenuController.Instance.ShowMainMenu();
            return;
        }
        ReservationsOverviewView.Instance.Display();
        int choice = int.Parse((string)ReservationsOverviewView.Instance.ViewBag["MainMenuSelection"]);
        if(choice == numberOfReservations + 1)
        {
            MainMenuController.Instance.ShowMainMenu();
        } else
        {
            Reservation? reservation = ReservationsOverviewView.Instance.ViewBag["reservation"] as Reservation;
            if (reservation != null)
            {
                ReservationController.Instance.ShowReservationToReservationOwner(reservation);
            } else {
                ConsoleUtils.Error("Er is iets fout gegaan.");
                ShowReservations();
            }
        }
    }

    /*
    *   LOGIN SECTION
    */



    /*
    *   REGISTER SECTION
    */

}