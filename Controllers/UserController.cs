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
        ReservationsOverviewView.Instance.Display();
        int choice = int.Parse((string)ReservationsOverviewView.Instance.ViewBag["MainMenuSelection"]);
        int numberOfReservations = UserManager.GetCurrentUser().GetReservationCount();
        if(choice == numberOfReservations + 2)
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