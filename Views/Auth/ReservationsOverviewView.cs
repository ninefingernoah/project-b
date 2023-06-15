public class ReservationsOverviewView : IView
{

    private static readonly ReservationsOverviewView instance = new ReservationsOverviewView();

    static ReservationsOverviewView()
    {
    }

    private ReservationsOverviewView()
    {
    }

    public static ReservationsOverviewView Instance
    {
        get
        {
            return instance;
        }
    }

    public Dictionary<string, object> ViewBag = new Dictionary<string, object>();

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

    public void PopulateViewBag()
    {

    }

    public void ClearViewBag()
    {

    }
}