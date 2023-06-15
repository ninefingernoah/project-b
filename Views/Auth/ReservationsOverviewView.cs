/// <summary>
/// View for displaying the reservations a user has. Singleton.
/// </summary>
public class ReservationsOverviewView : IView
{

    /// <summary>
    /// Singleton instance
    /// </summary>
    private static readonly ReservationsOverviewView instance = new ReservationsOverviewView();

    static ReservationsOverviewView()
    {
    }

    private ReservationsOverviewView()
    {
    }

    /// <summary>
    /// The getter for the singleton instance
    /// </summary>
    public static ReservationsOverviewView Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// The viewbag. Holds temporary data for the view.
    /// </summary>
    public Dictionary<string, object> ViewBag = new Dictionary<string, object>();

    /// <summary>
    /// Displays the view.
    /// </summary>
    public void Display()
    {
        User user = UserManager.GetCurrentUser()!;
        if (user == null)
        {
            ConsoleUtils.Error("U moet ingelogd zijn om uw reserveringen te bekijken.");
            return;
        }
        List<Reservation> reservations = user.GetReservations();
        List<string> options = new List<string>();
        foreach (Reservation reservation in reservations)
        {
            options.Add(reservation.ToString());
        }
        options.Add("-");
        options.Add("Ga terug");
        Menu overviewmenu = new Menu("Reserveringen", options.ToArray());
        int choice = overviewmenu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
        if(choice < reservations.Count + 2)
        {
            ViewBag["reservation"] = reservations[choice];
        }
    }

    /// <summary>
    /// Populates the viewbag with data.
    public void PopulateViewBag()
    {

    }

    /// <summary>
    /// Clears the viewbag.
    /// </summary>
    public void ClearViewBag()
    {

    }
}