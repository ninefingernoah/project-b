public class PassengerOverviewView : IView
{

    private static readonly PassengerOverviewView instance = new PassengerOverviewView();

    static PassengerOverviewView()
    {
    }

    private PassengerOverviewView()
    {
    }

    public static PassengerOverviewView Instance
    {
        get
        {
            return instance;
        }
    }

    public Dictionary<string, object> ViewBag = new Dictionary<string, object>();

    Passenger ThePassenger;

    public void Display()
    {
        List<string> optionsList = new List<string>() {
            $"Voornaam: {ThePassenger.FirstName}",
            $"Achternaam: {ThePassenger.LastName}",
            $"Email: {ThePassenger.Email}",
            $"Document nummer: {ThePassenger.DocumentNumber}",
            "-",
            "Terug"
        };
        
    }

    public void PopulateViewBag(Passenger passenger)
    {
       ThePassenger = passenger;
    }

    public void ClearViewBag()
    {

    }
}