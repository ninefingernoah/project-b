/// <summary>
/// View for displaying a passenger overview. Singleton.
/// </summary>
public class PassengerOverviewView : IView
{

    /// <summary>
    /// Singleton instance
    /// </summary>
    private static readonly PassengerOverviewView instance = new PassengerOverviewView();

    static PassengerOverviewView()
    {
    }

    /// <summary>
    /// The getter for the singleton instance
    /// </summary>
    public static PassengerOverviewView Instance
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
    /// The passenger that is being overviewed.
    /// </summary>
    public Passenger? ThePassenger;

    /// <summary>
    /// Displays the view.
    /// </summary>
    public void Display()
    {
        if (ThePassenger == null)
        {
            throw new Exception("Passenger is null");
        }
        List<string> optionsList = new List<string>() {
            $"Voornaam: {ThePassenger.FirstName}",
            $"Achternaam: {ThePassenger.LastName}",
            $"Email: {ThePassenger.Email}",
            $"Document nummer: {ThePassenger.DocumentNumber}",
            $"Geboortedatum: {((DateTime) ThePassenger.BirthDate!).ToString("dd-MM-yyyy")}",
            $"Adres: {ThePassenger.Address.Street} {ThePassenger.Address.HouseNumber}, {ThePassenger.Address.City}, {ThePassenger.Address.Country}",
            "-",
            "Terug"
        };
        Menu menu = new Menu("Passagier bewerken", optionsList.ToArray());
        int choice = menu.Run();
        ViewBag["MainMenuSelection"] = choice.ToString();
    }

    /// <summary>
    /// Populates the viewbag with the passenger that is being overviewed.
    /// </summary>
    public void PopulateViewBag(Passenger passenger)
    {
       ThePassenger = passenger;
    }

    /// <summary>
    /// Clears the viewbag.
    /// </summary>
    public void ClearViewBag()
    {
        ViewBag.Clear();
        ThePassenger = null;
    }
}